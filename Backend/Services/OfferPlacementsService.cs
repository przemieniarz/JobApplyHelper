using Backend.Data;
using Backend.Data.Models;
using Backend.ExceptionHandlers.Constants;
using Backend.ExceptionHandlers.Exceptions;
using Backend.Services.Extensions;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

using OfferPlacementApi = Backend.Api.Models.OfferPlacement;

namespace Backend.Services;

public class OfferPlacementsService(AppDbContext dbContext) : IOfferPlacementsService
{
    public async Task<OfferPlacementApi.CreateResponse> CreateAsync(OfferPlacementApi.CreateRequest request,
        CancellationToken cancelationToken)
    {
        //ToDo Add validation by name
        var newOfferPlacement = new OfferPlacement
        {
            Name = request.Name,
            Region = request.Region,
            CreatedDate = DateTimeOffset.Now,
            ModifiedDate = DateTimeOffset.Now,
        };

        await dbContext.OfferPlacements.AddAsync(newOfferPlacement, cancelationToken);
        await dbContext.SaveChangesAsync(cancelationToken);

        var response = new OfferPlacementApi.CreateResponse
        {
            Id = newOfferPlacement.Id,
            Name = newOfferPlacement.Name,
            Region = newOfferPlacement.Region,
            ModifiedDate = newOfferPlacement.ModifiedDate,
        };

        return response;
    }

    public async Task<OfferPlacementApi.UpdateResponse> UpdateAsync(OfferPlacementApi.UpdateRequest request,
        CancellationToken cancelationToken)
    {
        var offerPlacement = await GetByIdAsync(request.Id, cancelationToken);

        offerPlacement.Name = request.Name;
        offerPlacement.Region = request.Region;
        offerPlacement.ModifiedDate = DateTimeOffset.Now;

        //todo add modifieddate validation
        await dbContext.SaveChangesAsync(cancelationToken);

        var response = new OfferPlacementApi.UpdateResponse
        {
            Id = offerPlacement.Id,
            Name = offerPlacement.Name,
            Region = offerPlacement.Region,
            ModifiedDate = offerPlacement.ModifiedDate,
        };

        return response;
    }

    public async Task<OfferPlacementApi.GetByIdResponse> GetByIdAsync(OfferPlacementApi.GetByIdRequest request,
        CancellationToken cancelationToken)
    {
        var offerPlacement = await GetByIdAsync(request.Id, cancelationToken);

        var response = new OfferPlacementApi.GetByIdResponse
        {
            Id = offerPlacement.Id,
            Name = offerPlacement.Name,
            Region = offerPlacement.Region,
            ModifiedDate = offerPlacement.ModifiedDate,
        };

        return response;
    }

    public async Task<OfferPlacementApi.GetAllResponse> GetAllAsync(OfferPlacementApi.GetAllRequest request,
        CancellationToken cancelationToken)
    {
        var query = dbContext.OfferPlacements.AsQueryable();

        query = FilterQuery(query, request);
        var totalRows = await query.CountAsync(cancelationToken);

        query = SortQuery(query, request);
        query = PaginateQuery(query, request);

        var offerPlacements = await query.ToListAsync(cancelationToken);

        var offerPlacementsMapped = offerPlacements
            .Select(op => new OfferPlacementApi.GetAllOneItemResponse
            {
                Id = op.Id,
                Name = op.Name,
                Region = op.Region,
                ModifiedDate = op.ModifiedDate,
            }).ToList();

        return new OfferPlacementApi.GetAllResponse
        {
            Items = offerPlacementsMapped,
            TotalRows = totalRows,
        };
    }

    private async Task<OfferPlacement> GetByIdAsync(Guid id, CancellationToken cancelationToken)
    {
        var offerPlacement = await dbContext.OfferPlacements
            .FirstOrDefaultAsync(op => op.Id == id);

        if (offerPlacement == null)
        {
            throw new NotFoundException(
                string.Format(ExceptionMessages.EntityWithIdDoesntExist, nameof(OfferPlacement), id));
        }

        return offerPlacement;
    }

    private static IQueryable<OfferPlacement> FilterQuery(IQueryable<OfferPlacement> query,
        OfferPlacementApi.GetAllRequest request)
    {
        if (!string.IsNullOrEmpty(request.NameInclude))
        {
            query = query.Where(q => q.Name.ToLower().Contains(request.NameInclude.ToLower()));
        }

        if (request.RegionInclude != null)
        {
            query = query.Where(q => q.Region == request.RegionInclude);
        }

        return query;
    }

    private static IQueryable<OfferPlacement> SortQuery(IQueryable<OfferPlacement> query,
        OfferPlacementApi.GetAllRequest request)
    {
        if (request.SortBy == null || request.SortOrder == null)
        {
            return query;
        }

        var direction = request.SortOrder.ToDirectionString();
        
        query = query.OrderBy($"{request.SortBy} {direction}");

        return query;
    }

    private static IQueryable<OfferPlacement> PaginateQuery(IQueryable<OfferPlacement> query,
        OfferPlacementApi.GetAllRequest request)
    {
        if (request.PageOffset == null || request.PageLimit == null)
        {
            return query;
        }

        query = query
            .Skip((int)request.PageOffset)
            .Take((int)request.PageLimit);

        return query;
    }
}

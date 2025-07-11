using Backend.Data;
using Backend.Data.Models;
using Backend.Services.Extensions;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

    public Task<OfferPlacementApi.GetAllResponse> GetAllAsync(OfferPlacementApi.GetAllRequest request,
        CancellationToken cancelationToken)
    {
        //filtering
        var query = dbContext.OfferPlacements.AsQueryable();
        FilterQuery(query, request);

        //sorting
        

        //paginacja i wyciaganie all
    }

    private async Task<OfferPlacement> GetByIdAsync(Guid id, CancellationToken cancelationToken)
    {
        var offerPlacement = await dbContext.OfferPlacements
            .FirstOrDefaultAsync(op => op.Id == id);

        if (offerPlacement == null)
        {
            throw new ValidationException($"Offer placement with id {id} doesnt exist");
        }

        return offerPlacement;
    }

    private static void FilterQuery(IQueryable<OfferPlacement> query,
        OfferPlacementApi.GetAllRequest request)
    {
        if (!string.IsNullOrEmpty(request.NameInclude))
        {
            query = query.Where(q => q.NormalizedName.Contains(request.NameInclude));
        }

        if (request.RegionInclude != null)
        {
            query = query.Where(q => q.Region == request.RegionInclude);
        }
    }

    private static void SortQuery(IQueryable<OfferPlacement> query,
        OfferPlacementApi.GetAllRequest request)
    {
        if (request.SortBy == null || request.SortOrder == null)
        {
            return;
        }

        var direction = request.SortOrder.ToDirectionString();
        _ = query.OrderBy($"{request.SortBy} {direction}");
    }
}

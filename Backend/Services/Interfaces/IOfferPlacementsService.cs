using Backend.Api.Models;

namespace Backend.Services.Interfaces;

public interface IOfferPlacementsService
{
    Task<OfferPlacement.CreateResponse> CreateAsync(OfferPlacement.CreateRequest request,
        CancellationToken cancelationToken);
    Task<OfferPlacement.UpdateResponse> UpdateAsync(OfferPlacement.UpdateRequest request,
        CancellationToken cancelationToken);
    Task<OfferPlacement.GetByIdResponse> GetByIdAsync(OfferPlacement.GetByIdRequest request,
        CancellationToken cancelationToken);
    Task<OfferPlacement.GetAllResponse> GetAllAsync(OfferPlacement.GetAllRequest request,
        CancellationToken cancelationToken);
}

using Backend.Api.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;
[Route("admin/[controller]s")]
[ApiController]
public class OfferPlacementController(IOfferPlacementsService offerPlacementService) : ControllerBase
{
    [HttpPost]
    public Task<OfferPlacement.CreateResponse> CreateAsync([FromBody] OfferPlacement.CreateRequest request,
        CancellationToken cancelationToken)
    {
        var response = offerPlacementService.CreateAsync(request, cancelationToken);
        return response;
    }

    [HttpPut]
    public Task<OfferPlacement.UpdateResponse> UpdateAsync([FromBody] OfferPlacement.UpdateRequest request,
        CancellationToken cancelationToken)
    {
        var response = offerPlacementService.UpdateAsync(request, cancelationToken);
        return response;
    }

    [HttpGet("/{id}")]
    public Task<OfferPlacement.GetByIdResponse> GetByIdASync([FromRoute] Guid id,
        CancellationToken cancelationToken)
    {
        var request = new OfferPlacement.GetByIdRequest { Id = id };
        var response = offerPlacementService.GetByIdAsync(request, cancelationToken);
        return response;
    }

    [HttpGet]
    public Task<OfferPlacement.GetAllResponse> GetAllAsync([FromQuery] OfferPlacement.GetAllRequest request,
        CancellationToken cancelationToken)
    {
        var response = offerPlacementService.GetAllAsync(request, cancelationToken);
        return response;
    }
}

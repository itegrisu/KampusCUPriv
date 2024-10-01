using Application.Features.OfferManagementFeatures.Offers.Commands.Create;
using Application.Features.OfferManagementFeatures.Offers.Commands.Delete;
using Application.Features.OfferManagementFeatures.Offers.Commands.Update;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OfferManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : BaseController<CreateOfferCommand, DeleteOfferCommand, UpdateOfferCommand, GetByGidOfferQuery,
         CreatedOfferResponse, DeletedOfferResponse, UpdatedOfferResponse, GetByGidOfferResponse>
    {
        public OffersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOfferQuery getListOfferQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOfferListItemDto> response = await Mediator.Send(getListOfferQuery);
            return Ok(response);
        }
    }
}

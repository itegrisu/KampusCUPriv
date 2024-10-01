using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Create;
using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Delete;
using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Update;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OfferManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferTransactionsController : BaseController<CreateOfferTransactionCommand, DeleteOfferTransactionCommand, UpdateOfferTransactionCommand, GetByGidOfferTransactionQuery,
       CreatedOfferTransactionResponse, DeletedOfferTransactionResponse, UpdatedOfferTransactionResponse, GetByGidOfferTransactionResponse>
    {
        public OfferTransactionsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOfferTransactionQuery getListOfferTransactionQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOfferTransactionListItemDto> response = await Mediator.Send(getListOfferTransactionQuery);
            return Ok(response);
        }


    }
}

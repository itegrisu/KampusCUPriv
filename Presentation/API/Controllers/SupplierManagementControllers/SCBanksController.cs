using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCBanksController : BaseController<CreateSCBankCommand, DeleteSCBankCommand, UpdateSCBankCommand, GetByGidSCBankQuery,
         CreatedSCBankResponse, DeletedSCBankResponse, UpdatedSCBankResponse, GetByGidSCBankResponse>
    {
        public SCBanksController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSCBankQuery getListSCBankQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSCBankListItemDto> response = await Mediator.Send(getListSCBankQuery);
            return Ok(response);
        }


    }
}

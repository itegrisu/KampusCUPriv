using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCWorkHistoriesController : BaseController<CreateSCWorkHistoryCommand, DeleteSCWorkHistoryCommand, UpdateSCWorkHistoryCommand, GetByGidSCWorkHistoryQuery,
     CreatedSCWorkHistoryResponse, DeletedSCWorkHistoryResponse, UpdatedSCWorkHistoryResponse, GetByGidSCWorkHistoryResponse>
    {
        public SCWorkHistoriesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSCWorkHistoryQuery getListSCWorkHistoryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSCWorkHistoryListItemDto> response = await Mediator.Send(getListSCWorkHistoryQuery);
            return Ok(response);
        }


    }
}

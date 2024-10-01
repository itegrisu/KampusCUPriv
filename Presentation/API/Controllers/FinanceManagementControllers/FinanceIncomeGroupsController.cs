using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.UpdateRowNo;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.FinanceManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceIncomeGroupsController : BaseController<CreateFinanceIncomeGroupCommand, DeleteFinanceIncomeGroupCommand, UpdateFinanceIncomeGroupCommand, GetByGidFinanceIncomeGroupQuery,
         CreatedFinanceIncomeGroupResponse, DeletedFinanceIncomeGroupResponse, UpdatedFinanceIncomeGroupResponse, GetByGidFinanceIncomeGroupResponse>
    {
        public FinanceIncomeGroupsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceIncomeGroupQuery getListFinanceIncomeGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceIncomeGroupListItemDto> response = await Mediator.Send(getListFinanceIncomeGroupQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoFinanceIncomeGroupCommand command)
        {
            UpdateRowNoFinanceIncomeGroupResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}

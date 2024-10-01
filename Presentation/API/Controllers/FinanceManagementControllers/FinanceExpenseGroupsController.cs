using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.UpdateRowNo;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetList;
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
    public class FinanceExpenseGroupsController : BaseController<CreateFinanceExpenseGroupCommand, DeleteFinanceExpenseGroupCommand, UpdateFinanceExpenseGroupCommand, GetByGidFinanceExpenseGroupQuery,
          CreatedFinanceExpenseGroupResponse, DeletedFinanceExpenseGroupResponse, UpdatedFinanceExpenseGroupResponse, GetByGidFinanceExpenseGroupResponse>
    {
        public FinanceExpenseGroupsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceExpenseGroupQuery getListFinanceExpenseGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceExpenseGroupListItemDto> response = await Mediator.Send(getListFinanceExpenseGroupQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoFinanceExpenseGroupCommand command)
        {
            UpdateRowNoFinanceExpenseGroupResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}

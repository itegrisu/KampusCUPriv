using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.FinanceManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceExpensesController : BaseController<CreateFinanceExpenseCommand, DeleteFinanceExpenseCommand, UpdateFinanceExpenseCommand, GetByGidFinanceExpenseQuery,
         CreatedFinanceExpenseResponse, DeletedFinanceExpenseResponse, UpdatedFinanceExpenseResponse, GetByGidFinanceExpenseResponse>
    {
        public FinanceExpensesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceExpenseQuery getListFinanceExpenseQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceExpenseListItemDto> response = await Mediator.Send(getListFinanceExpenseQuery);
            return Ok(response);
        }


    }
}

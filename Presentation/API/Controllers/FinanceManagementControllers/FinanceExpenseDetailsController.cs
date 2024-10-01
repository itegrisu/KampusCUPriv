using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.FinanceManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceExpenseDetailsController : BaseController<CreateFinanceExpenseDetailCommand, DeleteFinanceExpenseDetailCommand, UpdateFinanceExpenseDetailCommand, GetByGidFinanceExpenseDetailQuery,
       CreatedFinanceExpenseDetailResponse, DeletedFinanceExpenseDetailResponse, UpdatedFinanceExpenseDetailResponse, GetByGidFinanceExpenseDetailResponse>
    {
        public FinanceExpenseDetailsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceExpenseDetailQuery getListFinanceExpenseDetailQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceExpenseDetailListItemDto> response = await Mediator.Send(getListFinanceExpenseDetailQuery);
            return Ok(response);
        }


    }
}

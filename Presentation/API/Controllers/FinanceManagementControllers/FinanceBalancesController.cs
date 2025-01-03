using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.UploadFile;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetBySCGidWithDateRange;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByUserGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.FinanceManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceBalancesController : BaseController<CreateFinanceBalanceCommand, DeleteFinanceBalanceCommand, UpdateFinanceBalanceCommand, GetByGidFinanceBalanceQuery,
        CreatedFinanceBalanceResponse, DeletedFinanceBalanceResponse, UpdatedFinanceBalanceResponse, GetByGidFinanceBalanceResponse>
    {
        public FinanceBalancesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceBalanceQuery getListFinanceBalanceQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceBalanceListItemDto> response = await Mediator.Send(getListFinanceBalanceQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBySCGidWithDateRange([FromBody] GetBySCGidWithDateRangeListFinanceBalanceQuery getBySCGidWithDateRangeListFinanceBalanceQuery)
        {
            GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto> response = await Mediator.Send(getBySCGidWithDateRangeListFinanceBalanceQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadBalanceFile([FromBody] UploadBalanceFileCommand uploadBalanceFileCommand)
        {
            UploadBalanceFileResponse response = await Mediator.Send(uploadBalanceFileCommand);
            return Ok(response);
        }

    }
}

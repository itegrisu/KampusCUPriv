using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.UploadFile;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetList;
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
    public class FinanceIncomesController : BaseController<CreateFinanceIncomeCommand, DeleteFinanceIncomeCommand, UpdateFinanceIncomeCommand, GetByGidFinanceIncomeQuery,
        CreatedFinanceIncomeResponse, DeletedFinanceIncomeResponse, UpdatedFinanceIncomeResponse, GetByGidFinanceIncomeResponse>
    {
        public FinanceIncomesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFinanceIncomeQuery getListFinanceIncomeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFinanceIncomeListItemDto> response = await Mediator.Send(getListFinanceIncomeQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFinanceIncome([FromBody] UploadFinanceIncomeCommand uploadFinanceIncomeCommand)
        {
            UploadFinanceIncomeResponse response = await Mediator.Send(uploadFinanceIncomeCommand);
            return Ok(response);
        }
    }
}

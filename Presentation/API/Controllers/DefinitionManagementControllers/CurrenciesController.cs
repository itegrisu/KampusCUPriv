using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : BaseController<CreateCurrencyCommand, DeleteCurrencyCommand, UpdateCurrencyCommand, GetByGidCurrencyQuery,
        CreatedCurrencyResponse, DeletedCurrencyResponse, UpdatedCurrencyResponse, GetByGidCurrencyResponse>
    {
        public CurrenciesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCurrencyQuery getListCurrencyQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCurrencyListItemDto> response = await Mediator.Send(getListCurrencyQuery);
            return Ok(response);
        }


    }
}

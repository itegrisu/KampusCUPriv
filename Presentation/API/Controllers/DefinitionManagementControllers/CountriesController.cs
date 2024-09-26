using Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Countries.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Countries.Commands.UpdateRowNo;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseController<CreateCountryCommand, DeleteCountryCommand, UpdateCountryCommand, GetByGidCountryQuery,
         CreatedCountryResponse, DeletedCountryResponse, UpdatedCountryResponse, GetByGidCountryResponse>
    {
        public CountriesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCountryQuery getListCountryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCountryListItemDto> response = await Mediator.Send(getListCountryQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoCountryCommand command)
        {
            UpdateRowNoCountryResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}

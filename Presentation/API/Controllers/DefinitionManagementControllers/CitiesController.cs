using Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Cities.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController<CreateCityCommand, DeleteCityCommand, UpdateCityCommand, GetByGidCityQuery,
       CreatedCityResponse, DeletedCityResponse, UpdatedCityResponse, GetByGidCityResponse>
    {
        public CitiesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCityQuery getListCityQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCityListItemDto> response = await Mediator.Send(getListCityQuery);
            return Ok(response);
        }


    }
}

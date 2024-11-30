using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByCountryGid;
using Application.Features.DefinitionManagementFeatures.Districts.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Districts.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Districts.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByCityGid;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : BaseController<CreateDistrictCommand, DeleteDistrictCommand, UpdateDistrictCommand, GetByGidDistrictQuery,
       CreatedDistrictResponse, DeletedDistrictResponse, UpdatedDistrictResponse, GetByGidDistrictResponse>
    {
        public DistrictsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListDistrictQuery getListDistrictQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListDistrictListItemDto> response = await Mediator.Send(getListDistrictQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCityGid([FromQuery] GetByCityGidListDistrictQuery getByCityGidListDistrictQuery)
        {
            GetListResponse<GetByCityGidListDistrictListItemDto> response = await Mediator.Send(getByCityGidListDistrictQuery);
            return Ok(response);
        }

    }
}

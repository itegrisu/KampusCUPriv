using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Create;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtoBrandsController : BaseController<CreateOtoBrandCommand, DeleteOtoBrandCommand, UpdateOtoBrandCommand, GetByGidOtoBrandQuery,
          CreatedOtoBrandResponse, DeletedOtoBrandResponse, UpdatedOtoBrandResponse, GetByGidOtoBrandResponse>
    {
        public OtoBrandsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOtoBrandQuery getListOtoBrandQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOtoBrandListItemDto> response = await Mediator.Send(getListOtoBrandQuery);
            return Ok(response);
        }


    }
}

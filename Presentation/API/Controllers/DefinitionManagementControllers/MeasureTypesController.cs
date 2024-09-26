using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureTypesController : BaseController<CreateMeasureTypeCommand, DeleteMeasureTypeCommand, UpdateMeasureTypeCommand, GetByGidMeasureTypeQuery,
         CreatedMeasureTypeResponse, DeletedMeasureTypeResponse, UpdatedMeasureTypeResponse, GetByGidMeasureTypeResponse>
    {
        public MeasureTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListMeasureTypeQuery getListMeasureTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListMeasureTypeListItemDto> response = await Mediator.Send(getListMeasureTypeQuery);
            return Ok(response);
        }


    }
}

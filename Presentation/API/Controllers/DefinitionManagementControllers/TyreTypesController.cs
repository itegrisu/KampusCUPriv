using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyreTypesController : BaseController<CreateTyreTypeCommand, DeleteTyreTypeCommand, UpdateTyreTypeCommand, GetByGidTyreTypeQuery,
       CreatedTyreTypeResponse, DeletedTyreTypeResponse, UpdatedTyreTypeResponse, GetByGidTyreTypeResponse>
    {
        public TyreTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTyreTypeQuery getListTyreTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTyreTypeListItemDto> response = await Mediator.Send(getListTyreTypeQuery);
            return Ok(response);
        }


    }
}

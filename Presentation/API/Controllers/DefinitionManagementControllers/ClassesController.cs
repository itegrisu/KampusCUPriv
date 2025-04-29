using Application.Features.DefinitionFeatures.Classes.Commands.Create;
using Application.Features.DefinitionFeatures.Classes.Commands.Delete;
using Application.Features.DefinitionFeatures.Classes.Commands.Update;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Classes.Queries.GetList;
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
    public class ClassesController : BaseController<CreateClassCommand, DeleteClassCommand, UpdateClassCommand, GetByGidClassQuery,
         CreatedClassResponse, DeletedClassResponse, UpdatedClassResponse, GetByGidClassResponse>
    {
        public ClassesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize]

        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListClassQuery getListClassQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListClassListItemDto> response = await Mediator.Send(getListClassQuery);
            return Ok(response);
        }


    }
}

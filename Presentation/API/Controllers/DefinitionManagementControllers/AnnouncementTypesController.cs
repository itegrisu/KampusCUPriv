using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Create;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Delete;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Update;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementTypesController : BaseController<CreateAnnouncementTypeCommand, DeleteAnnouncementTypeCommand, UpdateAnnouncementTypeCommand, GetByGidAnnouncementTypeQuery,
          CreatedAnnouncementTypeResponse, DeletedAnnouncementTypeResponse, UpdatedAnnouncementTypeResponse, GetByGidAnnouncementTypeResponse>
    {
        public AnnouncementTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAnnouncementTypeQuery getListAnnouncementTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAnnouncementTypeListItemDto> response = await Mediator.Send(getListAnnouncementTypeQuery);
            return Ok(response);
        }


    }
}

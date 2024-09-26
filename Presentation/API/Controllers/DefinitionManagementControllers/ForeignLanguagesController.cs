using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Update;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForeignLanguagesController : BaseController<CreateForeignLanguageCommand, DeleteForeignLanguageCommand, UpdateForeignLanguageCommand, GetByGidForeignLanguageQuery,
      CreatedForeignLanguageResponse, DeletedForeignLanguageResponse, UpdatedForeignLanguageResponse, GetByGidForeignLanguageResponse>
    {
        public ForeignLanguagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListForeignLanguageQuery getListForeignLanguageQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListForeignLanguageListItemDto> response = await Mediator.Send(getListForeignLanguageQuery);
            return Ok(response);
        }


    }
}

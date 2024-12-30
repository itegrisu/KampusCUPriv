using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartTimeWorkerForeignLanguagesController : BaseController<CreatePartTimeWorkerForeignLanguageCommand, DeletePartTimeWorkerForeignLanguageCommand, UpdatePartTimeWorkerForeignLanguageCommand, GetByGidPartTimeWorkerForeignLanguageQuery,
        CreatedPartTimeWorkerForeignLanguageResponse, DeletedPartTimeWorkerForeignLanguageResponse, UpdatedPartTimeWorkerForeignLanguageResponse, GetByGidPartTimeWorkerForeignLanguageResponse>
    {
        public PartTimeWorkerForeignLanguagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListPartTimeWorkerForeignLanguageQuery getListPartTimeWorkerForeignLanguageQuery)
        {
            GetListResponse<GetListPartTimeWorkerForeignLanguageListItemDto> response = await Mediator.Send(getListPartTimeWorkerForeignLanguageQuery);
            return Ok(response);
        }

    }
}

using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : BaseController<CreateDocumentTypeCommand, DeleteDocumentTypeCommand, UpdateDocumentTypeCommand, GetByGidDocumentTypeQuery,
        CreatedDocumentTypeResponse, DeletedDocumentTypeResponse, UpdatedDocumentTypeResponse, GetByGidDocumentTypeResponse>
    {
        public DocumentTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListDocumentTypeQuery getListDocumentTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListDocumentTypeListItemDto> response = await Mediator.Send(getListDocumentTypeQuery);
            return Ok(response);
        }


    }
}

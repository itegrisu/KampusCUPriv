using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelDocumentsController : BaseController<CreatePersonnelDocumentCommand, DeletePersonnelDocumentCommand, UpdatePersonnelDocumentCommand, GetByGidPersonnelDocumentQuery,
         CreatedPersonnelDocumentResponse, DeletedPersonnelDocumentResponse, UpdatedPersonnelDocumentResponse, GetByGidPersonnelDocumentResponse>
    {
        public PersonnelDocumentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelDocumentQuery getListPersonnelDocumentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelDocumentListItemDto> response = await Mediator.Send(getListPersonnelDocumentQuery);
            return Ok(response);
        }


    }
}

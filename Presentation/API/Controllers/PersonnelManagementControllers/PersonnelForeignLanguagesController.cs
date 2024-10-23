using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelForeignLanguagesController : BaseController<CreatePersonnelForeignLanguageCommand, DeletePersonnelForeignLanguageCommand, UpdatePersonnelForeignLanguageCommand, GetByGidPersonnelForeignLanguageQuery,
          CreatedPersonnelForeignLanguageResponse, DeletedPersonnelForeignLanguageResponse, UpdatedPersonnelForeignLanguageResponse, GetByGidPersonnelForeignLanguageResponse>
    {
        public PersonnelForeignLanguagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelForeignLanguageQuery getListPersonnelForeignLanguageQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelForeignLanguageListItemDto> response = await Mediator.Send(getListPersonnelForeignLanguageQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelForeignLanguageQuery getByUserGidListPersonnelForeignLanguageQuery)
        {
            GetListResponse<GetByUserGidListPersonnelForeignLanguageListItemDto> response = await Mediator.Send(getByUserGidListPersonnelForeignLanguageQuery);
            return Ok(response);
        }

    }
}

using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetList;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelGraduatedSchoolsController : BaseController<CreatePersonnelGraduatedSchoolCommand, DeletePersonnelGraduatedSchoolCommand, UpdatePersonnelGraduatedSchoolCommand, GetByGidPersonnelGraduatedSchoolQuery,
        CreatedPersonnelGraduatedSchoolResponse, DeletedPersonnelGraduatedSchoolResponse, UpdatedPersonnelGraduatedSchoolResponse, GetByGidPersonnelGraduatedSchoolResponse>
    {
        public PersonnelGraduatedSchoolsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelGraduatedSchoolQuery getListPersonnelGraduatedSchoolQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelGraduatedSchoolListItemDto> response = await Mediator.Send(getListPersonnelGraduatedSchoolQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelGraduatedSchoolQuery getByUserGidListPersonnelGraduatedSchoolQuery)
        {
            GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto> response = await Mediator.Send(getByUserGidListPersonnelGraduatedSchoolQuery);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UploadGraduatedSchoolFile([FromBody] UploadGraduatedSchoolFileCommand uploadGraduatedSchoolFileCommand)
        {
            UploadGraduatedSchoolFileResponse response = await Mediator.Send(uploadGraduatedSchoolFileCommand);
            return Ok(response);
        }

    }
}

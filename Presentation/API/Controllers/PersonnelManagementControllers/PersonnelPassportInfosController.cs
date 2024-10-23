using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelPassportInfosController : BaseController<CreatePersonnelPassportInfoCommand, DeletePersonnelPassportInfoCommand, UpdatePersonnelPassportInfoCommand, GetByGidPersonnelPassportInfoQuery,
          CreatedPersonnelPassportInfoResponse, DeletedPersonnelPassportInfoResponse, UpdatedPersonnelPassportInfoResponse, GetByGidPersonnelPassportInfoResponse>
    {
        public PersonnelPassportInfosController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelPassportInfoQuery getListPersonnelPassportInfoQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelPassportInfoListItemDto> response = await Mediator.Send(getListPersonnelPassportInfoQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelPassportInfoQuery getByUserGidListPersonnelPassportInfoQuery)
        {
            GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto> response = await Mediator.Send(getByUserGidListPersonnelPassportInfoQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadPassportFile([FromBody] UploadPassportFileCommand uploadPassportFileCommand)
        {
            UploadPassportFileResponse response = await Mediator.Send(uploadPassportFileCommand);
            return Ok(response);
        }

    }
}

using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelResidenceInfosController : BaseController<CreatePersonnelResidenceInfoCommand, DeletePersonnelResidenceInfoCommand, UpdatePersonnelResidenceInfoCommand, GetByGidPersonnelResidenceInfoQuery,
        CreatedPersonnelResidenceInfoResponse, DeletedPersonnelResidenceInfoResponse, UpdatedPersonnelResidenceInfoResponse, GetByGidPersonnelResidenceInfoResponse>
    {
        public PersonnelResidenceInfosController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelResidenceInfoQuery getListPersonnelResidenceInfoQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelResidenceInfoListItemDto> response = await Mediator.Send(getListPersonnelResidenceInfoQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelResidenceInfoQuery getByUserGidListPersonnelResidenceInfoQuery)
        {
            GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto> response = await Mediator.Send(getByUserGidListPersonnelResidenceInfoQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadResidenceFile([FromBody] UploadResidenceFileCommand uploadResidenceFileCommand)
        {
            UploadResidenceFileResponse response = await Mediator.Send(uploadResidenceFileCommand);
            return Ok(response);
        }
    }
}

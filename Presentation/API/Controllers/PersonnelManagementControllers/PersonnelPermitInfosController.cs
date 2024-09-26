using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelPermitInfosController : BaseController<CreatePersonnelPermitInfoCommand, DeletePersonnelPermitInfoCommand, UpdatePersonnelPermitInfoCommand, GetByGidPersonnelPermitInfoQuery,
       CreatedPersonnelPermitInfoResponse, DeletedPersonnelPermitInfoResponse, UpdatedPersonnelPermitInfoResponse, GetByGidPersonnelPermitInfoResponse>
    {
        public PersonnelPermitInfosController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelPermitInfoQuery getListPersonnelPermitInfoQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelPermitInfoListItemDto> response = await Mediator.Send(getListPersonnelPermitInfoQuery);
            return Ok(response);
        }


    }
}

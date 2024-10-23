using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelWorkingTablesController : BaseController<CreatePersonnelWorkingTableCommand, DeletePersonnelWorkingTableCommand, UpdatePersonnelWorkingTableCommand, GetByGidPersonnelWorkingTableQuery,
        CreatedPersonnelWorkingTableResponse, DeletedPersonnelWorkingTableResponse, UpdatedPersonnelWorkingTableResponse, GetByGidPersonnelWorkingTableResponse>
    {
        public PersonnelWorkingTablesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelWorkingTableQuery getListPersonnelWorkingTableQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelWorkingTableListItemDto> response = await Mediator.Send(getListPersonnelWorkingTableQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelWorkingTableQuery getByUserGidListPersonnelWorkingTableQuery)
        {
            GetListResponse<GetByUserGidListPersonnelWorkingTableListItemDto> response = await Mediator.Send(getByUserGidListPersonnelWorkingTableQuery);
            return Ok(response);
        }

    }
}

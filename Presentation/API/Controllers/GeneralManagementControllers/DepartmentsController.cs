using Application.Features.GeneralManagementFeatures.Departments.Commands.Create;
using Application.Features.GeneralManagementFeatures.Departments.Commands.Delete;
using Application.Features.GeneralManagementFeatures.Departments.Commands.Update;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetTaskCountList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<CreateDepartmentCommand, DeleteDepartmentCommand, UpdateDepartmentCommand, GetByGidDepartmentQuery,
        CreatedDepartmentResponse, DeletedDepartmentResponse, UpdatedDepartmentResponse, GetByGidDepartmentResponse>
    {
        public DepartmentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListDepartmentQuery getListDepartmentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListDepartmentListItemDto> response = await Mediator.Send(getListDepartmentQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> GetListWithUser([FromQuery] GetListWithUserDepartmentQuery request)
        {
            GetListResponse<GetListWithUserDepartmentListItemDto> response = await Mediator.Send(request);

            return Ok(response);
        }

    }
}

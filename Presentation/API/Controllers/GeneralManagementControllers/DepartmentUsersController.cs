using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Delete;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Update;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentUsersController : BaseController<CreateDepartmentUserCommand, DeleteDepartmentUserCommand, UpdateDepartmentUserCommand, GetByGidDepartmentUserQuery,
         CreatedDepartmentUserResponse, DeletedDepartmentUserResponse, UpdatedDepartmentUserResponse, GetByGidDepartmentUserResponse>
    {
        public DepartmentUsersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListDepartmentUserQuery getListDepartmentUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListDepartmentUserListItemDto> response = await Mediator.Send(getListDepartmentUserQuery);
            return Ok(response);
        }


    }
}

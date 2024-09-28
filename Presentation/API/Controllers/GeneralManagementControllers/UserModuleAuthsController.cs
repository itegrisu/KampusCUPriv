using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModuleAuthsController : BaseController<CreateUserModuleAuthCommand, DeleteUserModuleAuthCommand, UpdateUserModuleAuthCommand, GetByGidUserModuleAuthQuery,
      CreatedUserModuleAuthResponse, DeletedUserModuleAuthResponse, UpdatedUserModuleAuthResponse, GetByGidUserModuleAuthResponse>
    {
        public UserModuleAuthsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserModuleAuthQuery getListUserModuleAuthQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserModuleAuthListItemDto> response = await Mediator.Send(getListUserModuleAuthQuery);
            return Ok(response);
        }


    }
}

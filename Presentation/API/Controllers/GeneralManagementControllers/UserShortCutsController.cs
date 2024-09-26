using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.UpdateRowNo;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserShortCutsController : BaseController<CreateUserShortCutCommand, DeleteUserShortCutCommand, UpdateUserShortCutCommand, GetByGidUserShortCutQuery,
        CreatedUserShortCutResponse, DeletedUserShortCutResponse, UpdatedUserShortCutResponse, GetByGidUserShortCutResponse>
    {
        public UserShortCutsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserShortCutQuery getListUserShortCutQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserShortCutListItemDto> response = await Mediator.Send(getListUserShortCutQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetByUserGidItemList([FromQuery] GetByUserGidShortCutQuery getByUserGidShortCutQuery)
        {
            GetListResponse<GetByUserGidShortCutListItemDto> response = await Mediator.Send(getByUserGidShortCutQuery);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Sort([FromBody] UpdateRowNoUserShortCutCommand command)
        {
            UpdateRowNoUserShortCutResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}

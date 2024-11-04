using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.Users.Commands.ChangeIsActive;
using Application.Features.GeneralManagementFeatures.Users.Commands.Create;
using Application.Features.GeneralManagementFeatures.Users.Commands.Delete;
using Application.Features.GeneralManagementFeatures.Users.Commands.Update;
using Application.Features.GeneralManagementFeatures.Users.Commands.UpdateForAdmin;
using Application.Features.GeneralManagementFeatures.Users.Commands.UpdatePersonnelSpecialNote;
using Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatar;
using Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatarTemp;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByEnum;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetCompanyEmployee;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetListDeleted;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetSystemAdmin;
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
    public class UsersController : BaseController<CreateUserCommand, DeleteUserCommand, UpdateUserCommand, GetByGidUserQuery,
        CreatedUserResponse, DeletedUserResponse, UpdatedUserResponse, GetByGidUserResponse>
    {
        public UsersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetListComboBox([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);
            return Ok(response);
        }




        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetListDeleted([FromQuery] PageRequest pageRequest)
        {
            GetListDeletedUserQuery getListDeletedUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListDeletedUserListItemDto> response = await Mediator.Send(getListDeletedUserQuery);
            return Ok(response);
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadAvatar([FromBody] UploadAvatarUserCommand uploadAvatarUserCommand)
        {
            UploadAvatarUserResponse response = await Mediator.Send(uploadAvatarUserCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadAvatarTemp([FromQuery] string gid)
        {
            UploadAvatarTempCommand uploadAvatarTempCommand = new()
            {
                Params = gid,
                FormFiles = Request.Form.Files
            };
            UploadAvatarTempResponse response = await Mediator.Send(uploadAvatarTempCommand);
            return Ok(response);
        }

        //[HttpGet("[action]/{Gid}")]
        //public async Task<IActionResult> GetByGid([FromRoute] GetByGidUserQuery getByGidUserQuery)
        //{
        //    GetByGidUserResponse response = await Mediator.Send(getByGidUserQuery);
        //    return Ok(response);
        //}

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetSystemAdmin([FromQuery] PageRequest pageRequest)
        {
            GetSystemAdminUserQuery getSystemAdminUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetSystemAdminUserListItemDto> response = await Mediator.Send(getSystemAdminUserQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UpdateForAdmin([FromBody] UpdateForAdminUserCommand updateForAdminUserCommand)
        {
            UpdateForAdminUserResponse response = await Mediator.Send(updateForAdminUserCommand);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeIsActiveUser([FromRoute] ChangeIsActiveUserCommand changeIsActiveUserCommand)
        {
            ChangeIsActiveUserResponse response = await Mediator.Send(changeIsActiveUserCommand);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UpdatePersonnelSpecialNoteUser([FromBody] UpdatePersonnelSpecialNoteUserCommand updatePersonnelSpecialNoteUserCommand)
        {
            UpdatePersonnelSpecialNoteUserResponse response = await Mediator.Send(updatePersonnelSpecialNoteUserCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetCompanyEmployee([FromQuery] PageRequest pageRequest)
        {
            GetCompanyEmployeeUserQuery getCompanyEmployeeUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetCompanyEmployeeUserListItemDto> response = await Mediator.Send(getCompanyEmployeeUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByEnumUser([FromQuery] GetByEnumUserQuery getByEnumUserQuery)
        {
            GetListResponse<GetByEnumUserListItemDto> response = await Mediator.Send(getByEnumUserQuery);
            return Ok(response);
        }

    }
}

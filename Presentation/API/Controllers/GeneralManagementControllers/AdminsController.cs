using Application.Features.GeneralFeatures.Admins.Commands.Create;
using Application.Features.GeneralFeatures.Admins.Commands.Delete;
using Application.Features.GeneralFeatures.Admins.Commands.Update;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Application.Features.GeneralFeatures.Admins.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Admins.Commands.Login;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : BaseController<CreateAdminCommand, DeleteAdminCommand, UpdateAdminCommand, GetByGidAdminQuery,
      CreatedAdminResponse, DeletedAdminResponse, UpdatedAdminResponse, GetByGidAdminResponse>
    {
        public AdminsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAdminQuery getListAdminQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAdminListItemDto> response = await Mediator.Send(getListAdminQuery);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand loginAdminCommand)
        {
            LoginAdminResponse response = await Mediator.Send(loginAdminCommand);
            return Ok(response);
        }
    }
}

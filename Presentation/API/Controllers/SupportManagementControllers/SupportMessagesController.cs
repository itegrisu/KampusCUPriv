using API.Filters;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadFile;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadTempFile;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupportManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportMessagesController : BaseController<CreateSupportMessageCommand, DeleteSupportMessageCommand, UpdateSupportMessageCommand, GetByGidSupportMessageQuery,
        CreatedSupportMessageResponse, DeletedSupportMessageResponse, UpdatedSupportMessageResponse, GetByGidSupportMessageResponse>
    {
        public SupportMessagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetListBySupportGid([FromQuery] GetListSupportMessageQuery getListSupportMessageQuery)
        {
            GetListResponse<GetListSupportMessageListItemDto> response = await Mediator.Send(getListSupportMessageQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> SupportFile([FromBody] UploadSupportFileCommand uploadSupportFileCommand)
        {
            UploadSupportFileResponse response = await Mediator.Send(uploadSupportFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> SupportFileTemp([FromQuery] string gid)
        {
            UploadSupportFileTempCommand uploadAvatarTempCommand = new()
            {
                Params = gid,
                FormFiles = Request.Form.Files
            };
            UploadSupportFileTempResponse response = await Mediator.Send(uploadAvatarTempCommand);
            return Ok(response);
        }


    }

}

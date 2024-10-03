using Application.Features.FileUploadManagementFeatures.UploadFileTemp;
using Application.Features.FileUploadManagementFeatures.UploadImageTemp;
using Application.Features.FileUploadManagementFeatures.UploadPdfTemp;
using Application.Features.GeneralManagementFeatures.UploadFileTemp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadTempController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadTempController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> UploadImageTemp()
        {
            UploadImageTempCommand command = new()
            {
                FormFiles = Request.Form.Files
            };

            UploadImageTempResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFileTemp()
        {
            UploadFileTempCommand command = new()
            {
                FormFiles = Request.Form.Files,
            };
            UploadFileTempResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadPdfTemp()
        {
            UploadPdfTempCommand command = new()
            {
                FormFiles = Request.Form.Files
            };
            UploadPdfTempResponse response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}

using Application.Features.StockManagementFeatures.StockCardImages.Commands.Create;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.Delete;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.Update;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.UpdateRowNo;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.UploadFile;
using Application.Features.StockManagementFeatures.StockCardImages.Commands.UploadFileTemp;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StockManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockCardImagesController : ControllerBase
    {

        protected readonly IMediator Mediator;
        protected readonly clsAuth _clsAuth;

        public StockCardImagesController(IMediator mediator, clsAuth clsAuth)
        {
            Mediator = mediator;
            _clsAuth = clsAuth;
        }



        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] GetListStockCardImageQuery getListStockCardImageQuery)
        {
            GetListResponse<GetListStockCardImageListItemDto> response = await Mediator.Send(getListStockCardImageQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoStockCardImageCommand command)
        {
            UpdateRowNoStockCardImageResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadStockCardImage([FromBody] UploadStockCardImageCommand command)
        {
            UploadStockCardImageResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadStockCardImageTemp([FromQuery] string gid)
        {
            UploadStockCardImageTempCommand command = new()
            {
                Params = gid,
                FormFiles = Request.Form.Files
            };
            UploadStockCardImageTempResponse response = await Mediator.Send(command);
            return Ok(response);
        }
        //Crud işlemleri ***********************************************************************************************************

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Add([FromBody] CreateStockCardImageCommand request)
        {
            CreatedStockCardImageResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Update([FromBody] UpdateStockCardImageCommand request)
        {
            UpdatedStockCardImageResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteStockCardImageCommand deleteCommand)
        {
            DeletedStockCardImageResponse response = await Mediator.Send(deleteCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGid([FromQuery] GetByGidStockCardImageQuery getByIdQuery)
        {
            GetByGidStockCardImageResponse response = await Mediator.Send(getByIdQuery);
            return Ok(response);
        }


    }
}

using Application.Features.ProjectManagementFeatures.ProjectFiles.Commands;
using Application.Features.StockManagementFeatures.StockMovements.Commands.Create;
using Application.Features.StockManagementFeatures.StockMovements.Commands.Delete;
using Application.Features.StockManagementFeatures.StockMovements.Commands.Update;
using Application.Features.StockManagementFeatures.StockMovements.Commands.UploadFile;
using Application.Features.StockManagementFeatures.StockMovements.Commands.UploadFileTemp;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StockManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : ControllerBase
    {

        protected readonly IMediator Mediator;
        protected readonly clsAuth _clsAuth;

        public StockMovementsController(IMediator mediator, clsAuth clsAuth)
        {
            Mediator = mediator;
            _clsAuth = clsAuth;
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] GetListStockMovementQuery getListStockMovement)
        {
            GetListResponse<GetListStockMovementListItemDto> response = await Mediator.Send(getListStockMovement);
            return Ok(response);
        }

        //Crud işlemleri ***********************************************************************************************************

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Add([FromBody] CreateStockMovementCommand request)
        {
            CreatedStockMovementResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Update([FromBody] UpdateStockMovementCommand request)
        {
            UpdatedStockMovementResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteStockMovementCommand deleteCommand)
        {
            DeletedStockMovementResponse response = await Mediator.Send(deleteCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGid([FromQuery] GetByGidStockMovementQuery getByIdQuery)
        {
            GetByGidStockMovementResponse response = await Mediator.Send(getByIdQuery);
            return Ok(response);
        }



        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadStockMovement([FromBody] UploadStockMovementCommand uploadFileCommand)
        {
            UploadStockMovementResponse response = await Mediator.Send(uploadFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadStockMovementTemp([FromQuery] string gid)
        {
            UploadStockMovementTempCommand uploadAvatarTempCommand = new()
            {
                Params = gid,
                FormFiles = Request.Form.Files
            };
            UploadStockMovementTempResponse response = await Mediator.Send(uploadAvatarTempCommand);
            return Ok(response);
        }



    }
}

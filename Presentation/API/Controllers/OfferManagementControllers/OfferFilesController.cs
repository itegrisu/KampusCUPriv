using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Create;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Delete;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Update;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OfferManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferFilesController : BaseController<CreateOfferFileCommand, DeleteOfferFileCommand, UpdateOfferFileCommand, GetByGidOfferFileQuery,
         CreatedOfferFileResponse, DeletedOfferFileResponse, UpdatedOfferFileResponse, GetByGidOfferFileResponse>
    {
        public OfferFilesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOfferFileQuery getListOfferFileQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOfferFileListItemDto> response = await Mediator.Send(getListOfferFileQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByOfferGid([FromQuery] GetByOfferGidListOfferFileQuery getByOfferGidListOfferFileQuery)
        {
            GetListResponse<GetByOfferGidListOfferFileListItemDto> response = await Mediator.Send(getByOfferGidListOfferFileQuery);
            return Ok(response);
        }


    }
}

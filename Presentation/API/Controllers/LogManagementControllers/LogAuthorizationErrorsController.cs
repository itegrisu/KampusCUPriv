using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Create;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Delete;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Update;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LogManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAuthorizationErrorsController : BaseController<CreateLogAuthorizationErrorCommand, DeleteLogAuthorizationErrorCommand, UpdateLogAuthorizationErrorCommand, GetByGidLogAuthorizationErrorQuery,
         CreatedLogAuthorizationErrorResponse, DeletedLogAuthorizationErrorResponse, UpdatedLogAuthorizationErrorResponse, GetByGidLogAuthorizationErrorResponse>
    {
        public LogAuthorizationErrorsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetList([FromBody] GetListLogAuthorizationErrorQuery query)
        {
            GetListResponse<GetListLogAuthorizationErrorListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }


    }
}

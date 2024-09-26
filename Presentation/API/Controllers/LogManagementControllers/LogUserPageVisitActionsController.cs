using API.Filters;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Create;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Delete;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Update;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LogManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogUserPageVisitActionsController : BaseController<CreateLogUserPageVisitActionCommand, DeleteLogUserPageVisitActionCommand, UpdateLogUserPageVisitActionCommand, GetByGidLogUserPageVisitActionQuery,
        CreatedLogUserPageVisitActionResponse, DeletedLogUserPageVisitActionResponse, UpdatedLogUserPageVisitActionResponse, GetByGidLogUserPageVisitActionResponse>
    {
        public LogUserPageVisitActionsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromBody] GetListLogUserPageVisitActionQuery query)
        {

            GetListResponse<GetListLogUserPageVisitActionListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }


    }
}

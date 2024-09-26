using API.Filters;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Create;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Delete;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Update;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetBySessionId;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using Infrastracture.Services.LogUserPageVisit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LogManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogUserPageVisitsController : BaseController<CreateLogUserPageVisitCommand, DeleteLogUserPageVisitCommand, UpdateLogUserPageVisitCommand, GetByGidLogUserPageVisitQuery,
        CreatedLogUserPageVisitResponse, DeletedLogUserPageVisitResponse, UpdatedLogUserPageVisitResponse, GetByGidLogUserPageVisitResponse>
    {

        private readonly LogUserPageVisitService _logUserPageVisitService;

        public LogUserPageVisitsController(IMediator mediator, clsAuth clsAuth, LogUserPageVisitService logUserPageVisitService) : base(mediator, clsAuth)
        {
            _logUserPageVisitService = logUserPageVisitService;
        }

        [HttpPost("[action]")]
        
        public async Task<IActionResult> GetList([FromBody] GetListLogUserPageVisitQuery query)
        {
            GetListResponse<GetListLogUserPageVisitListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> LogPageVisit()
        {
            await _logUserPageVisitService.LogUserPageVisit();
            return Ok();
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetBySession([FromQuery] GetBySessionIdLogUserPageVisitQuery query)
        {
            GetListResponse<GetBySessionIdLogUserPageVisitListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetByFilter([FromBody] GetListLogUserPageVisitQuery request)
        {
            GetListResponse<GetListLogUserPageVisitListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByFilterComboBox([FromBody] GetListLogUserPageVisitQuery request)
        {
            GetListResponse<GetListLogUserPageVisitListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> AddLog([FromBody] CreateLogUserPageVisitCommand request)
        {
            CreatedLogUserPageVisitResponse response = await Mediator.Send(request);
            return Ok(response);
        }

    }
}

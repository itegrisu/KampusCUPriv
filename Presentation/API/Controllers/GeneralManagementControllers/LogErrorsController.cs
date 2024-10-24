using API.Filters;
using Application.Abstractions.EntityServices;
using Application.Dtos.OtherDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogErrorsController : ControllerBase
    {
        private readonly ILogService _logErrorService;
        public LogErrorsController(ILogService logErrorService)
        {
            _logErrorService = logErrorService;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult GetLogErrors([FromQuery] Guid? userGid, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            List<LogErrorDto> response = _logErrorService.GetLogErrors(userGid, startTime, endTime);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> LogOutLog([FromQuery] Guid gid)
        {
            await _logErrorService.LogOutLog(gid);
            return Ok();
        }
    }
}

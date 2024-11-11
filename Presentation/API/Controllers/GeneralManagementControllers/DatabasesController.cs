using API.Filters;
using Application.Abstractions.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabasesController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public DatabasesController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public ActionResult<List<string>> GetAllTable()
        {
            var tableNames = _databaseService.GetTableNames();
            return Ok(tableNames);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult Update([FromQuery] string tableName, [FromQuery] Guid userGid, [FromQuery] Guid gid, [FromBody] Dictionary<string, object> data)
        {
            var baseResponse = _databaseService.UpdateData(tableName, userGid, gid, data);
            return Ok(baseResponse);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public ActionResult<List<ColumnData>> GetTableData([FromQuery] string tableName, [FromQuery] Guid gid)
        {
            var tableData = _databaseService.GetTableData(tableName, gid);
            return Ok(tableData);
        }
    }
}

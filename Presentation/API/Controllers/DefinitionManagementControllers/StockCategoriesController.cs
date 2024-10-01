using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Create;
using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Update;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockCategoriesController : BaseController<CreateStockCategoryCommand, DeleteStockCategoryCommand, UpdateStockCategoryCommand, GetByGidStockCategoryQuery,
        CreatedStockCategoryResponse, DeletedStockCategoryResponse, UpdatedStockCategoryResponse, GetByGidStockCategoryResponse>
    {
        public StockCategoriesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListStockCategoryQuery getListStockCategoryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListStockCategoryListItemDto> response = await Mediator.Send(getListStockCategoryQuery);
            return Ok(response);
        }


    }
}

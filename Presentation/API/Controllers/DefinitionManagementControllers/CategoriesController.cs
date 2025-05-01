using Application.Features.DefinitionFeatures.Categories.Commands.Create;
using Application.Features.DefinitionFeatures.Categories.Commands.Delete;
using Application.Features.DefinitionFeatures.Categories.Commands.Update;
using Application.Features.DefinitionFeatures.Categories.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Categories.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<CreateCategoryCommand, DeleteCategoryCommand, UpdateCategoryCommand, GetByGidCategoryQuery,
         CreatedCategoryResponse, DeletedCategoryResponse, UpdatedCategoryResponse, GetByGidCategoryResponse>
    {
        public CategoriesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCategoryListItemDto> response = await Mediator.Send(getListCategoryQuery);
            return Ok(response);
        }
    }
}

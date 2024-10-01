using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Update;

public class UpdatedStockCategoryResponse : BaseResponse, IResponse
{
    public GetByGidStockCategoryResponse Obj { get; set; }
}
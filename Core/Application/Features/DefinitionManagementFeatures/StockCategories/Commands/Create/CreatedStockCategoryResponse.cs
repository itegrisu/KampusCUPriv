using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Create;

public class CreatedStockCategoryResponse : BaseResponse, IResponse
{
    public GetByGidStockCategoryResponse Obj { get; set; }
}
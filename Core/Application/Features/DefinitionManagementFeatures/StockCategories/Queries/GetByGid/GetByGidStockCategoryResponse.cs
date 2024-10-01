using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetByGid
{
    public class GetByGidStockCategoryResponse : IResponse
    {
        public Guid Gid { get; set; }

public string Name { get; set; }
public string? Code { get; set; }

    }
}
using Application.Features.Base;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Update;

public class UpdatedStockCardResponse : BaseResponse, IResponse
{
    public GetByGidStockCardResponse Obj { get; set; }
}
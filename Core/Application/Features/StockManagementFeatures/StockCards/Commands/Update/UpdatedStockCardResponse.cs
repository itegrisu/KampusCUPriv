using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Update;

public class UpdatedStockCardResponse : BaseResponse, IResponse
{
    public GetByGidStockCardResponse Obj { get; set; }
}
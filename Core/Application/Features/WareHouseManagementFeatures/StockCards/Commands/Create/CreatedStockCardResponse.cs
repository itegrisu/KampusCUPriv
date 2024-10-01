using Application.Features.Base;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Create;

public class CreatedStockCardResponse : BaseResponse, IResponse
{
    public GetByGidStockCardResponse Obj { get; set; }
}
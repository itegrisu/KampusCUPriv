using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockCards.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Create;

public class CreatedStockCardResponse : BaseResponse, IResponse
{
    public GetByGidStockCardResponse Obj { get; set; }
}
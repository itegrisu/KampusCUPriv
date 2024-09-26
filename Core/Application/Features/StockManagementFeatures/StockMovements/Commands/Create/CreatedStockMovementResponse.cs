using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Create;

public class CreatedStockMovementResponse : BaseResponse, IResponse
{
    public GetByGidStockMovementResponse Obj { get; set; }
}
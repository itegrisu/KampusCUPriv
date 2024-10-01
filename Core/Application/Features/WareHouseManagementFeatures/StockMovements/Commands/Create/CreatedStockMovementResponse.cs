using Application.Features.Base;
using Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Commands.Create;

public class CreatedStockMovementResponse : BaseResponse, IResponse
{
    public GetByGidStockMovementResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Commands.Update;

public class UpdatedStockMovementResponse : BaseResponse, IResponse
{
    public GetByGidStockMovementResponse Obj { get; set; }
}
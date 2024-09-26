using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Update;

public class UpdatedStockMovementResponse : BaseResponse, IResponse
{
    public GetByGidStockMovementResponse Obj { get; set; }
}
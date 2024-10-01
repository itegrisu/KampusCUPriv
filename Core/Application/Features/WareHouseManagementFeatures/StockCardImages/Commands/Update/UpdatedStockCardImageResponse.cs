using Application.Features.Base;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Update;

public class UpdatedStockCardImageResponse : BaseResponse, IResponse
{
    public GetByGidStockCardImageResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Update;

public class UpdatedStockCardImageResponse : BaseResponse, IResponse
{
    public GetByGidStockCardImageResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Create;

public class CreatedStockCardImageResponse : BaseResponse, IResponse
{
    public GetByGidStockCardImageResponse Obj { get; set; }
}
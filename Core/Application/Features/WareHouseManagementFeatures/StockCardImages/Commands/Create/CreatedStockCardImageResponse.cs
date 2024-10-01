using Application.Features.Base;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Create;

public class CreatedStockCardImageResponse : BaseResponse, IResponse
{
    public GetByGidStockCardImageResponse Obj { get; set; }
}
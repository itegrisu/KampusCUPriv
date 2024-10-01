using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Commands.UploadFile
{
    public class UploadStockMovementResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}

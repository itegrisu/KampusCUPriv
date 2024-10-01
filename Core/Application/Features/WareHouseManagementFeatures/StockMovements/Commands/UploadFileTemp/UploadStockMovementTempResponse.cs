using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Commands.UploadFileTemp
{
    public class UploadStockMovementTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

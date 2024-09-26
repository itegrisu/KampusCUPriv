using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.UploadFileTemp
{
    public class UploadStockMovementTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

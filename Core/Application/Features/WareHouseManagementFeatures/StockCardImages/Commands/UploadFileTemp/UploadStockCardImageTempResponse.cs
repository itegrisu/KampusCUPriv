using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.UploadFileTemp
{
    public class UploadStockCardImageTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

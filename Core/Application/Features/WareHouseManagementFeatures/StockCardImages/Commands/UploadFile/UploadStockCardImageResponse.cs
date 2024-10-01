using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.UploadFile
{
    public class UploadStockCardImageResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}

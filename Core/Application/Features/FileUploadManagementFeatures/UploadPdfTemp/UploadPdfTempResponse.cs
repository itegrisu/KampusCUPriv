using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.FileUploadManagementFeatures.UploadPdfTemp
{
    public class UploadPdfTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

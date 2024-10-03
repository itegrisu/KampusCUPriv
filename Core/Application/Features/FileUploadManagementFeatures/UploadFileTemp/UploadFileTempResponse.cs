using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.FileUploadManagementFeatures.UploadFileTemp
{
    public class UploadFileTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

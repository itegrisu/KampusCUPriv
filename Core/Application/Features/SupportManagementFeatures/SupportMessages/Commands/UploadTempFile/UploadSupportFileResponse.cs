using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadTempFile
{
    public class UploadSupportFileTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

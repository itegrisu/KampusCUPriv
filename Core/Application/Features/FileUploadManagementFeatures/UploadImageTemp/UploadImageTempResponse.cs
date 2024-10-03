using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.UploadFileTemp
{
    public class UploadImageTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

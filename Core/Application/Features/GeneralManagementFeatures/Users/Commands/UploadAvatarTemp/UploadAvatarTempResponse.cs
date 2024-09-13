using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatarTemp
{
    public class UploadAvatarTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

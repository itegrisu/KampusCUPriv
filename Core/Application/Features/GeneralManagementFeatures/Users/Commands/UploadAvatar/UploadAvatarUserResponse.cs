using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatar
{
    public class UploadAvatarUserResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}

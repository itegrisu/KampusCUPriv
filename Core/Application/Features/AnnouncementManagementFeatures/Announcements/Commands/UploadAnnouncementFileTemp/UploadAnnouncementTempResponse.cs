using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFileTemp
{
    public class UploadAnnouncementTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}

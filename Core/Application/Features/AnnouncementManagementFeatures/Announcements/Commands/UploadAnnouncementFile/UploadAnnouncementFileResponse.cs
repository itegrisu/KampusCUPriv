using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFile
{
    public class UploadAnnouncementFileResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}

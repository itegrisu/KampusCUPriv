using Application.Features.Base;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;

public class UpdatedStudentAnnouncementResponse : BaseResponse, IResponse
{
    public GetByGidStudentAnnouncementResponse Obj { get; set; }
}
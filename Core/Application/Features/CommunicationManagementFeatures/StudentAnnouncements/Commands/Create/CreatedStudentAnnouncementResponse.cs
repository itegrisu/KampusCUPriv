using Application.Features.Base;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;

public class CreatedStudentAnnouncementResponse : BaseResponse, IResponse
{
    public GetByGidStudentAnnouncementResponse Obj { get; set; }
}
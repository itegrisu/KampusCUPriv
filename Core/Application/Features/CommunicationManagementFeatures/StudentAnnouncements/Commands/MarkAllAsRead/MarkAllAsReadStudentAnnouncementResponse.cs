using Application.Features.Base;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Commands.MarkAllAsRead
{
    public class MarkAllAsReadStudentAnnouncementResponse : BaseResponse, IResponse
    {
        public List<GetByGidStudentAnnouncementResponse> obj { get; set; }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Commands.MarkAllAsRead
{
    public class MarkAllAsReadStudentAnnouncementCommandValidator : AbstractValidator<MarkAllAsReadStudentAnnouncementCommand>
    {
        public MarkAllAsReadStudentAnnouncementCommandValidator()
        {
        }
    }
}

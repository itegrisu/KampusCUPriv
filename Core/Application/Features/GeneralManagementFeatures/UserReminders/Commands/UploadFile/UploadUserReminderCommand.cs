using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.UploadFile
{
    public class UploadUserReminderCommand : IRequest<UploadUserReminderResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
  
}

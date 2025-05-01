using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.EntityServices
{
    public interface INotificationService
    {
        public Task SendAsync(string title,string content,  List<Guid> UserGids, ProcessType processType);
    }
}

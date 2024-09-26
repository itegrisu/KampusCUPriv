using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SupportManagements
{
    public class SupportMessageDetail : BaseEntity
    {
        public Guid GidMessageFK  { get; set; }
        public SupportMessage SupportMessageFK { get; set; } 
        public Guid GidReadUserFK { get; set; }
        public User UserFK { get; set; }
        public DateTime ReadDate { get; set; }
        public string ReadIp { get; set; } = string.Empty;
    }
}

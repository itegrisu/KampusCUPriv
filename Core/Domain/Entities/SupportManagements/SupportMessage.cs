using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SupportManagements
{
    public class SupportMessage : BaseEntity
    {

        public Guid? GidSupportFK { get; set; }
        public SupportRequest SupportRequestFK { get; set; }
        public Guid GidSenderUserFK { get; set; }
        public User UserFK { get; set; }
        public string Message { get; set; } = string.Empty;
        public EnumMessageType MessageType { get; set; }
        public ICollection<SupportMessageDetail>? SupportMessageDetails  { get; set; } 


    }
}

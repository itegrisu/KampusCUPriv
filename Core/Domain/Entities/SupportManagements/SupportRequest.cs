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
    public class SupportRequest : BaseEntity
    {
        public Guid CreatedUserFK  { get; set; }
        public User UserFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public EnumSupportStatus SupportStatus { get; set; }
        public EnumPriorityType PriorityType { get; set; }
        public EnumSupportType SupportType { get; set; }

        public ICollection<SupportMessage>? SupportMessages { get; set; }

    }
}

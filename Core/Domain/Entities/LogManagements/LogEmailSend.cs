using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LogManagements
{
    public class LogEmailSend : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}

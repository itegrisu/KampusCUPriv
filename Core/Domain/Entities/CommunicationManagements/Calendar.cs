using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CommunicationManagements
{
    public class Calendar : BaseEntity
    {
        public Guid GidEventFK { get; set; }
        public Event EventFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Color { get; set; }
    }
}

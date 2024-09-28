using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MarketingManagements
{
    public class MarketingCustomer : BaseEntity
    {

        public string FullName { get; set; } = string.Empty;
        public string? Company { get; set; }
        public string Duty { get; set; } = string.Empty;
        public string? PreviousDuty { get; set; }
        public string? Gsm { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }

        public ICollection<MarketingVisitPlan>? MarketingVisitPlans { get; set; }

    }
}

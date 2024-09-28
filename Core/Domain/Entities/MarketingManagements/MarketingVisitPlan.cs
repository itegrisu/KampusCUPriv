using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.MarketingManagements
{
    public class MarketingVisitPlan : BaseEntity
    {
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidVisitCustomerFK { get; set; }
        public MarketingCustomer MarketingCustomerFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PlanningVisitDate { get; set; }
        public string? Description { get; set; }
        public EnumVisitStatus VisitStatus { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitRank { get; set; }
        public string? VisitNote { get; set; }
    }
}

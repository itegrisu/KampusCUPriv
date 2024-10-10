using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByYearList
{
    public class GetByYearListMarketingVisitPlanListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidVisitCustomerFK { get; set; }
        public string MarketingCustomerFKFullName { get; set; }
        public string Title { get; set; }
        public DateTime PlanningVisitDate { get; set; }
        public string? Description { get; set; }
        public EnumVisitStatus VisitStatus { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitRank { get; set; }
        public string? VisitNote { get; set; }

    }
}

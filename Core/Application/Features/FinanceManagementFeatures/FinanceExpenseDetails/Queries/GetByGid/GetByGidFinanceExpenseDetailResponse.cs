using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid
{
    public class GetByGidFinanceExpenseDetailResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidExpenseFK { get; set; }
        public string FinanceExpenseFKTitle { get; set; }
        public Guid GidSpendPersonnelFK { get; set; }
        public string SpendPersonnelFKFullName { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public string CurrencyFKName { get; set; }
        public Guid? GidControlPersonnelFK { get; set; }
        public string ControlPersonnelFKFullName { get; set; }
        public string SpentTitle { get; set; }
        public decimal Fee { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public EnumApprovalStatus ApprovalStatus { get; set; }
        public DateTime ControlDate { get; set; }
        public string? ControlDescription { get; set; }

    }
}
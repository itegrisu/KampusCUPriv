using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid
{
    public class GetByGidFinanceExpenseResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidExpenseGroupFK { get; set; }
        public string FinanceExpenseGroupFKName { get; set; }
        public Guid? GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public Guid GidMoneySenderPersonnelFK { get; set; }
        public string MoneySenderPersonnelFKFullName { get; set; }
        public Guid GidMoneyReceivePersonnelFK { get; set; }
        public string MoneyReceivePersonnelFKFullName { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public string CurrencyFKName { get; set; }
        public Guid? GidApprovalReceiverFK { get; set; }
        public string ApprovalReceiverFKFullName { get; set; }
        public string Title { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime TransactionDate { get; set; }
        public EnumExpenseStatus ExpenseStatus { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public EnumReceiverAcceptStatus ReceiverAcceptStatus { get; set; }
        public DateTime ReceiverAcceptDate { get; set; }
        public DateTime ReceiverRejectDate { get; set; }
        public string? ReceiverIpAddress { get; set; }

    }
}
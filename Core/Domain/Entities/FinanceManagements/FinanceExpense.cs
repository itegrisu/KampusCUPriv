using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Enums;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceExpense : BaseEntity
    {

        public Guid GidExpenseGroupFK { get; set; }
        public FinanceExpenseGroup FinanceExpenseGroupFK { get; set; }
        public Guid? GidOrganizationFK { get; set; }
        public Organization? OrganizationFK { get; set; }
        public Guid GidMoneySenderPersonnelFK { get; set; }
        public User MoneySenderPersonnelFK { get; set; }
        public Guid GidMoneyReceivePersonnelFK { get; set; }
        public User MoneyReceivePersonnelFK { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }
        public Guid? GidApprovalReceiverFK { get; set; }
        public User? ApprovalReceiverFK { get; set; }

        public string Title { get; set; } = string.Empty;
        public decimal AmountSpent { get; set; }
        public DateTime TransactionDate { get; set; }
        public EnumExpenseStatus ExpenseStatus { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public EnumReceiverAcceptStatus ReceiverAcceptStatus { get; set; }
        public DateTime? ReceiverAcceptDate { get; set; }
        public DateTime? ReceiverRejectDate { get; set; }
        public string? ReceiverIpAddress { get; set; }

        public ICollection<FinanceExpenseDetail>? FinanceExpenseDetails { get; set; }

    }
}

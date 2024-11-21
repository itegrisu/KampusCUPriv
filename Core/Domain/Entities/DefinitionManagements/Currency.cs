using Core.Entities;
using Domain.Entities.FinanceManagements;
using Domain.Entities.OfferManagements;
using Domain.Entities.SupplierCustomerManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? Symbol { get; set; }

        public ICollection<SCBank>? SCBanks { get; set; }
        public ICollection<FinanceExpense> FinanceExpenses { get; set; }
        public ICollection<FinanceExpenseDetail> FinanceExpenseDetails { get; set; }
        public ICollection<FinanceIncome> FinanceIncomes { get; set; }
        public ICollection<OfferTransaction> OfferTransactions { get; set; }
        public ICollection<FinanceBalance>? FinanceBalances { get; set; }
    }
}

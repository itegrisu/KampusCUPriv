using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceExpenseDetail : BaseEntity
    {

        public Guid GidExpenseFK { get; set; }
        public FinanceExpense FinanceExpenseFK { get; set; }
        public Guid GidSpendPersonnelFK { get; set; }
        public User SpendPersonnelFK { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }
        public Guid? GidControlPersonnelFK { get; set; }
        public User? ControlPersonnelFK { get; set; }
        public string SpentTitle { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public EnumApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ControlDate { get; set; }
        public string? ControlDescription { get; set; }


    }
}

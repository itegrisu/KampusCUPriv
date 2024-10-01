using Core.Entities;
using Domain.Enums;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceExpenseGroup : BaseEntity, IHasRowNo
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EnumExpenseGroupStatus ExpenseGroupStatus { get; set; }
        public int RowNo { get; set; }
        public ICollection<FinanceExpense>? FinanceExpenses { get; set; }

    }
}

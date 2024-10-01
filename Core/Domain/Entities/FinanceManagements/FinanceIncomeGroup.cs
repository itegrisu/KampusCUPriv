using Core.Entities;
using Domain.Enums;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceIncomeGroup : BaseEntity, IHasRowNo
    {


        public string IncomeGroupName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EnumIncomeGroupStatus IncomeGroupStatus { get; set; }
        public int RowNo { get; set; }
        public ICollection<FinanceIncome>? FinanceIncomes { get; set; }

    }
}

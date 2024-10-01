using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;

namespace Domain.Entities.FinanceManagements
{
    public class FinanceIncome : BaseEntity
    {

        public Guid GidIncomeGroupFK { get; set; }
        public FinanceIncomeGroup FinanceIncomeGroupFK { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }

        public string Title { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public DateTime MaturityDate { get; set; }
        public EnumIncomeStatus IncomeStatus { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}

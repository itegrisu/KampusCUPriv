using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetList;

public class GetListFinanceIncomeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidIncomeGroupFK { get; set; }
    public string FinanceIncomeGroupFKIncomeGroupName { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string CurrencyFKName { get; set; }
    public string Title { get; set; }
    public decimal Fee { get; set; }
    public DateTime MaturityDate { get; set; }
    public EnumIncomeStatus IncomeStatus { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }


}
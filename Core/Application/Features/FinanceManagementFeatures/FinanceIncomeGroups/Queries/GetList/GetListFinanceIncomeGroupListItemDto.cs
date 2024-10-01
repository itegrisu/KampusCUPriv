using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetList;

public class GetListFinanceIncomeGroupListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string IncomeGroupName { get; set; }
    public string? Description { get; set; }
    public EnumIncomeGroupStatus IncomeGroupStatus { get; set; }
    public int RowNo { get; set; }


}
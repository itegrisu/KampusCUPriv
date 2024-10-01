using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetList;

public class GetListFinanceExpenseGroupListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public EnumExpenseGroupStatus ExpenseGroupStatus { get; set; }
    public int RowNo { get; set; }


}
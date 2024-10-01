using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid
{
    public class GetByGidFinanceExpenseGroupResponse : IResponse
    {
        public Guid Gid { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public EnumExpenseGroupStatus ExpenseGroupStatus { get; set; }
        public int RowNo { get; set; }

    }
}
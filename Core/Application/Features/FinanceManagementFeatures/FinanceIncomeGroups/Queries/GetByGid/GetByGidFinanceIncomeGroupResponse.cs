using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid
{
    public class GetByGidFinanceIncomeGroupResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string IncomeGroupName { get; set; }
        public string? Description { get; set; }
        public EnumIncomeGroupStatus IncomeGroupStatus { get; set; }
        public int RowNo { get; set; }

    }
}
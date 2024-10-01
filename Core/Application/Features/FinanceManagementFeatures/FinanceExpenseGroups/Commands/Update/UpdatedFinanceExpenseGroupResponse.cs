using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Update;

public class UpdatedFinanceExpenseGroupResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseGroupResponse Obj { get; set; }
}
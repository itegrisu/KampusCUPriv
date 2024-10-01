using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Update;

public class UpdatedFinanceIncomeGroupResponse : BaseResponse, IResponse
{
    public GetByGidFinanceIncomeGroupResponse Obj { get; set; }
}
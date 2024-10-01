using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Update;

public class UpdatedFinanceExpenseDetailResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseDetailResponse Obj { get; set; }
}
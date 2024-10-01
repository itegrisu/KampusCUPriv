using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Update;

public class UpdatedFinanceExpenseResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseResponse Obj { get; set; }
}
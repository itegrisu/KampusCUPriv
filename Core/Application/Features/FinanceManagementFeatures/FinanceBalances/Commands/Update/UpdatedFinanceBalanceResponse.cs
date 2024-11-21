using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Update;

public class UpdatedFinanceBalanceResponse : BaseResponse, IResponse
{
    public GetByGidFinanceBalanceResponse Obj { get; set; }
}
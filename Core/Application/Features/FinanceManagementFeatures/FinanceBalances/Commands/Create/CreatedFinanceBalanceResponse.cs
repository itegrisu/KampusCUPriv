using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Create;

public class CreatedFinanceBalanceResponse : BaseResponse, IResponse
{
    public GetByGidFinanceBalanceResponse Obj { get; set; }
}
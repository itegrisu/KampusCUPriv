using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Create;

public class CreatedFinanceExpenseResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseResponse Obj { get; set; }
}
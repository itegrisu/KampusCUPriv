using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Create;

public class CreatedFinanceExpenseDetailResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseDetailResponse Obj { get; set; }
}
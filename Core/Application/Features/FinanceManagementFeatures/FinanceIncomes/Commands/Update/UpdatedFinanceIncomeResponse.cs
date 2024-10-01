using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Update;

public class UpdatedFinanceIncomeResponse : BaseResponse, IResponse
{
    public GetByGidFinanceIncomeResponse Obj { get; set; }
}
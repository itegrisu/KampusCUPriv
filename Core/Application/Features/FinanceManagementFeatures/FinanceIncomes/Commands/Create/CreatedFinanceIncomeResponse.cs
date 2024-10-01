using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Create;

public class CreatedFinanceIncomeResponse : BaseResponse, IResponse
{
    public GetByGidFinanceIncomeResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;

public class CreatedFinanceIncomeGroupResponse : BaseResponse, IResponse
{
    public GetByGidFinanceIncomeGroupResponse Obj { get; set; }
}
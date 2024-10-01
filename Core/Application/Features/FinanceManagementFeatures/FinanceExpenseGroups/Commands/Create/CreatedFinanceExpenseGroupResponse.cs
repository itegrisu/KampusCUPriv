using Application.Features.Base;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;

public class CreatedFinanceExpenseGroupResponse : BaseResponse, IResponse
{
    public GetByGidFinanceExpenseGroupResponse Obj { get; set; }
}
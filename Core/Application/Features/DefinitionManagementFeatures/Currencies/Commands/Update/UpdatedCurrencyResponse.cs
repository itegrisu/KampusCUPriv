using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;

public class UpdatedCurrencyResponse : BaseResponse, IResponse
{
    public GetByGidCurrencyResponse Obj { get; set; }
}
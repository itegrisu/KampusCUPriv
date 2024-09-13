using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;

public class CreatedCurrencyResponse : BaseResponse, IResponse
{
    public GetByGidCurrencyResponse Obj { get; set; }
}
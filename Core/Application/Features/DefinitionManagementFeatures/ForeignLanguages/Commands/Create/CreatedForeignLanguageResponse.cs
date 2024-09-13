using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;

public class CreatedForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidForeignLanguageResponse Obj { get; set; }
}
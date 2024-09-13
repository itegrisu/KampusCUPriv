using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Update;

public class UpdatedForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidForeignLanguageResponse Obj { get; set; }
}
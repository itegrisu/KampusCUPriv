using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;

public class UpdatedDocumentTypeResponse : BaseResponse, IResponse
{
    public GetByGidDocumentTypeResponse Obj { get; set; }
}
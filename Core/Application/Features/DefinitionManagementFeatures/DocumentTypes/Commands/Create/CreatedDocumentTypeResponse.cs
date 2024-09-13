using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;

public class CreatedDocumentTypeResponse : BaseResponse, IResponse
{
    public GetByGidDocumentTypeResponse Obj { get; set; }
}
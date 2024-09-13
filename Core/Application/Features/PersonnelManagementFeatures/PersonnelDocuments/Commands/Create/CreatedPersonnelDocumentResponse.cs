using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Create;

public class CreatedPersonnelDocumentResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelDocumentResponse Obj { get; set; }
}
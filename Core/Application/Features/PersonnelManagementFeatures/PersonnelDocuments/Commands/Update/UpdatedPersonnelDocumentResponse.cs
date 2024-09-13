using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Update;

public class UpdatedPersonnelDocumentResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelDocumentResponse Obj { get; set; }
}
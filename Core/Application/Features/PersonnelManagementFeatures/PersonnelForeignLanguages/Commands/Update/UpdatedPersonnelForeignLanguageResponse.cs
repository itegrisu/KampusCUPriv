using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;

public class UpdatedPersonnelForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelForeignLanguageResponse Obj { get; set; }
}
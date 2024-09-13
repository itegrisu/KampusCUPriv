using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;

public class CreatedPersonnelForeignLanguageResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelForeignLanguageResponse Obj { get; set; }
}
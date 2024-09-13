using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Create;

public class CreatedPersonnelGraduatedSchoolResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelGraduatedSchoolResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Update;

public class UpdatedPersonnelGraduatedSchoolResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelGraduatedSchoolResponse Obj { get; set; }
}
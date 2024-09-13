using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Update;

public class UpdatedPersonnelResidenceInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelResidenceInfoResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Update;

public class UpdatedPersonnelPermitInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelPermitInfoResponse Obj { get; set; }
}
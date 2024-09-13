using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Update;

public class UpdatedPersonnelPassportInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelPassportInfoResponse Obj { get; set; }
}
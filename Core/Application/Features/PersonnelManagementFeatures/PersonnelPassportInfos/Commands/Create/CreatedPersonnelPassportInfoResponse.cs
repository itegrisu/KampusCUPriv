using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;

public class CreatedPersonnelPassportInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelPassportInfoResponse Obj { get; set; }
}
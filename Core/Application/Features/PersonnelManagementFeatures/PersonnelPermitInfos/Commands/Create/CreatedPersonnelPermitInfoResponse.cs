using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;

public class CreatedPersonnelPermitInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelPermitInfoResponse Obj { get; set; }
}
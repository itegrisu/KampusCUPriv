using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;

public class CreatedPersonnelResidenceInfoResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelResidenceInfoResponse Obj { get; set; }
}
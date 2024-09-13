using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Create;

public class CreatedPersonnelAddressResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelAddressResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Update;

public class UpdatedPersonnelAddressResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelAddressResponse Obj { get; set; }
}
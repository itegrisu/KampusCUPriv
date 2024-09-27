using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Create;

public class CreatedSCAddressResponse : BaseResponse, IResponse
{
    public GetByGidSCAddressResponse Obj { get; set; }
}
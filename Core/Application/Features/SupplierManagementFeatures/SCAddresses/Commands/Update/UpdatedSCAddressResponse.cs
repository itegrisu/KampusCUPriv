using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Update;

public class UpdatedSCAddressResponse : BaseResponse, IResponse
{
    public GetByGidSCAddressResponse Obj { get; set; }
}
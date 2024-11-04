using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Update;

public class UpdatedSCPersonnelResponse : BaseResponse, IResponse
{
    public GetByGidSCPersonnelResponse Obj { get; set; }
}
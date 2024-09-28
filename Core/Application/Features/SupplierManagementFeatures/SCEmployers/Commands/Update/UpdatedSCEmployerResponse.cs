using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Update;

public class UpdatedSCEmployerResponse : BaseResponse, IResponse
{
    public GetByGidSCEmployerResponse Obj { get; set; }
}
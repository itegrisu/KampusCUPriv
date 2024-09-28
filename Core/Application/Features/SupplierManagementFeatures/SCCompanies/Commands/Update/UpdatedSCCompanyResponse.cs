using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Update;

public class UpdatedSCCompanyResponse : BaseResponse, IResponse
{
    public GetByGidSCCompanyResponse Obj { get; set; }
}
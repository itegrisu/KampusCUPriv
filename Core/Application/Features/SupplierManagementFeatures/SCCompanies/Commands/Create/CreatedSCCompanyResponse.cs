using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Create;

public class CreatedSCCompanyResponse : BaseResponse, IResponse
{
    public GetByGidSCCompanyResponse Obj { get; set; }
}
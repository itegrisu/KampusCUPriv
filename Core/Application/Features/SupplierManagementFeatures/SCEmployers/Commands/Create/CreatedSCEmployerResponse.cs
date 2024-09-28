using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Create;

public class CreatedSCEmployerResponse : BaseResponse, IResponse
{
    public GetByGidSCEmployerResponse Obj { get; set; }
}
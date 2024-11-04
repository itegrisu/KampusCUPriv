using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Create;

public class CreatedSCPersonnelResponse : BaseResponse, IResponse
{
    public GetByGidSCPersonnelResponse Obj { get; set; }
}
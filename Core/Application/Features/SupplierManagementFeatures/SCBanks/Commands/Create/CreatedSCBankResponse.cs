using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Create;

public class CreatedSCBankResponse : BaseResponse, IResponse
{
    public GetByGidSCBankResponse Obj { get; set; }
}
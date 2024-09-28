using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Update;

public class UpdatedSCBankResponse : BaseResponse, IResponse
{
    public GetByGidSCBankResponse Obj { get; set; }
}
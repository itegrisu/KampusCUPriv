using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Update;

public class UpdatedSCWorkHistoryResponse : BaseResponse, IResponse
{
    public GetByGidSCWorkHistoryResponse Obj { get; set; }
}
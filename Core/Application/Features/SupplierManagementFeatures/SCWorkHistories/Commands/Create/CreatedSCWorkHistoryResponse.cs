using Application.Features.Base;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Create;

public class CreatedSCWorkHistoryResponse : BaseResponse, IResponse
{
    public GetByGidSCWorkHistoryResponse Obj { get; set; }
}
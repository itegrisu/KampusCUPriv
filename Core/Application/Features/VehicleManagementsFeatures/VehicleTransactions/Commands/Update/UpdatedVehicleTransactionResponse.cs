using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Update;

public class UpdatedVehicleTransactionResponse : BaseResponse, IResponse
{
    public GetByGidVehicleTransactionResponse Obj { get; set; }
}
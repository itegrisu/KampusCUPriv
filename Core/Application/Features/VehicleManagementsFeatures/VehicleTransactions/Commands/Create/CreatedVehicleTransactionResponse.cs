using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;

public class CreatedVehicleTransactionResponse : BaseResponse, IResponse
{
    public GetByGidVehicleTransactionResponse Obj { get; set; }
}
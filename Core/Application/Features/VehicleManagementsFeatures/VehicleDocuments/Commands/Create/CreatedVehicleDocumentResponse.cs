using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Create;

public class CreatedVehicleDocumentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleDocumentResponse Obj { get; set; }
}
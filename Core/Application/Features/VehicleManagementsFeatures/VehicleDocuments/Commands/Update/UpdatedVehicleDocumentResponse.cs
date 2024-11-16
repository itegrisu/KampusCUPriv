using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Update;

public class UpdatedVehicleDocumentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleDocumentResponse Obj { get; set; }
}
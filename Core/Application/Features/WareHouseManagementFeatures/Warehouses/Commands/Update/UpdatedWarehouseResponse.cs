using Application.Features.Base;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Update;

public class UpdatedWarehouseResponse : BaseResponse, IResponse
{
    public GetByGidWarehouseResponse Obj { get; set; }
}
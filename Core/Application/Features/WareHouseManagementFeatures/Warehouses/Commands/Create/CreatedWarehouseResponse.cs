using Application.Features.Base;
using Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Create;

public class CreatedWarehouseResponse : BaseResponse, IResponse
{
    public GetByGidWarehouseResponse Obj { get; set; }
}
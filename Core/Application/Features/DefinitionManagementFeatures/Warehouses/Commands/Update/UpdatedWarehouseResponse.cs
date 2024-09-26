using Application.Features.Base;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Update;

public class UpdatedWarehouseResponse : BaseResponse, IResponse
{
    public GetByGidWarehouseResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Create;

public class CreatedWarehouseResponse : BaseResponse, IResponse
{
    public GetByGidWarehouseResponse Obj { get; set; }
}
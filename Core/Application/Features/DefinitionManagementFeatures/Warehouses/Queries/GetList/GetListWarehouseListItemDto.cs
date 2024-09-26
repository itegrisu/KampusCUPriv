using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetList;

public class GetListWarehouseListItemDto : IDto
{
    public Guid Gid { get; set; }

public string WarehouseName { get; set; }
public string Location { get; set; }


}
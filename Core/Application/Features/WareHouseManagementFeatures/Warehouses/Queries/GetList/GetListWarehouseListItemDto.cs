using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetList;

public class GetListWarehouseListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidOrganizationFK { get; set; }
    public string OrganizationFKOrganizationName { get; set; }
    public string Name { get; set; }
    public EnumWarehouseType WarehouseType { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }


}
using Core.Application.Responses;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid
{
    public class GetByGidWarehouseResponse : IResponse
    {
        public Guid Gid { get; set; }

public string WarehouseName { get; set; }
public string Location { get; set; }

    }
}
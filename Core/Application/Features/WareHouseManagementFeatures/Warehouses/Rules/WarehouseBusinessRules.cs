using Application.Features.WarehouseManagementFeatures.Warehouses.Constants;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.WarehouseManagements;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Rules;

public class WarehouseBusinessRules : BaseBusinessRules
{
    //public Guid GidOrganizationFK { get; set; }
    //public string Name
    //{
    //    get; set;
    //}

    private readonly IOrganizationReadRepository _organizationReadRepository;
    private readonly IWarehouseReadRepository _warehouseReadRepository;

    public WarehouseBusinessRules(IOrganizationReadRepository organizationReadRepository, IWarehouseReadRepository warehouseReadRepository)
    {
        _organizationReadRepository = organizationReadRepository;
        _warehouseReadRepository = warehouseReadRepository;
    }

    public async Task WarehouseShouldExistWhenSelected(X.Warehouse? item)
    {
        if (item == null)
            throw new BusinessException(WarehousesBusinessMessages.WarehouseNotExists);
    }

    public async Task OrganizationShouldExistWhenSelected(string? gidOrganizationFK)
    {


        var organization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == new Guid(gidOrganizationFK));
        if (gidOrganizationFK != null && organization == null)
            throw new BusinessException(WarehousesBusinessMessages.OrganizationNotExists);
    }

    public async Task WarehouseNameShouldBeUnique(string name, Guid? gid = null)
    {
        Warehouse warehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Name == name && x.Gid != gid);
        if (warehouse != null)
            throw new BusinessException(WarehousesBusinessMessages.WarehouseNameShouldBeUnique);
    }

}
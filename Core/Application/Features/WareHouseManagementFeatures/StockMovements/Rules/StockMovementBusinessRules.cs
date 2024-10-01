using Application.Features.WareHouseManagementFeatures.StockMovements.Constants;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.WarehouseManagements;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Rules;

public class StockMovementBusinessRules : BaseBusinessRules
{

    private readonly IStockCardReadRepository _stockCardReadRepository;
    private readonly IWarehouseReadRepository _warehouseReadRepository;

    public async Task StockMovementShouldExistWhenSelected(StockMovement? item)
    {
        if (item == null)
            throw new BusinessException(StockMovementsBusinessMessages.StockMovementNotExists);
    }

    public async Task StockCardExistWhenSelected(Guid gid)
    {
        StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (stockCard == null)
            throw new BusinessException(StockMovementsBusinessMessages.StockCardNotExists);
    }

    public async Task PreviousWarehouseExistWhenSelected(string gid)
    {
        if (gid.IsNullOrEmpty() && await _warehouseReadRepository.GetAsync(predicate: x => x.Gid.ToString() == gid) == null)
            throw new BusinessException(StockMovementsBusinessMessages.PreviousWarehouseNotExists);
    }

    public async Task NextWarehouseExistWhenSelected(string gid)
    {
        if (gid.IsNullOrEmpty() && await _warehouseReadRepository.GetAsync(predicate: x => x.Gid.ToString() == gid) == null)
            throw new BusinessException(StockMovementsBusinessMessages.NextWarehouseNotExists);
    }
}
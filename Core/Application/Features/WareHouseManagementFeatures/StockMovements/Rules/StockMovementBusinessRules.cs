using Application.Features.WareHouseManagementFeatures.StockMovements.Constants;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
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
    private readonly IStockMovementReadRepository _stockMovementReadRepository;

    public StockMovementBusinessRules(IStockCardReadRepository stockCardReadRepository, IWarehouseReadRepository warehouseReadRepository, IStockMovementReadRepository stockMovementReadRepository)
    {
        _stockCardReadRepository = stockCardReadRepository;
        _warehouseReadRepository = warehouseReadRepository;
        _stockMovementReadRepository = stockMovementReadRepository;
    }

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

    public async Task IsThereStockInWarehouseForStockOutput(Guid stockCardGid, Guid? productWarehouseGid, int amount)
    {
        if (productWarehouseGid == null)
            throw new BusinessException(StockMovementsBusinessMessages.PreviousWarehouseNotExists);

        List<StockMovement> entryStockMovements = _stockMovementReadRepository.GetAll().Where(x => x.GidNextWarehouseFK == productWarehouseGid && x.GidStockCardFK == stockCardGid).ToList();
        List<StockMovement> outputStockMovements = _stockMovementReadRepository.GetAll().Where(
            x => x.GidNextWarehouseFK != productWarehouseGid && x.GidNextWarehouseFK != null && x.GidPreviousWarehouseFK == productWarehouseGid && x.GidStockCardFK == stockCardGid).ToList();
        //Ýlgili depodaki giriþler ve çýkýþlar hesaplanýp çýkarýlacak. Böylece o an o depoda kaç tane var hesaplanmýþ olacak.

        int countInEntryWarehouse = 0;
        foreach (var stockMovement in entryStockMovements)
        {
            countInEntryWarehouse += stockMovement.Amount;
        }

        int countOutEntryWarehouse = 0;
        foreach (var stockMovement in outputStockMovements)
        {
            countOutEntryWarehouse += stockMovement.Amount;
        }

        if ((countInEntryWarehouse - countOutEntryWarehouse) < amount)
        {
            throw new BusinessException(StockMovementsBusinessMessages.LessProductInWarehouse);
        }
    }

}
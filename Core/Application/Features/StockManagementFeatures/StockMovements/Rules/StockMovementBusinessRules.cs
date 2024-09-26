using Application.Features.StockManagementFeatures.StockMovements.Constants;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using Application.Repositories.StockManagementRepos.StockCardRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.IdentityModel.Tokens;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockMovements.Rules;

public class StockMovementBusinessRules : BaseBusinessRules
{

    private readonly IStockCardReadRepository _stockCardReadRepository;
    private readonly IWarehouseReadRepository _warehouseReadRepository;

    public async Task StockMovementShouldExistWhenSelected(X.StockMovement? item)
    {
        if (item == null)
            throw new BusinessException(StockMovementsBusinessMessages.StockMovementNotExists);
    }

    public async Task StockCardExistWhenSelected(Guid gid)
    {
        X.StockCard? stockCard = await _stockCardReadRepository.GetAsync(predicate: x => x.Gid == gid);
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
using Application.Features.StockManagementFeatures.StockCards.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCards.Rules;

public class StockCardBusinessRules : BaseBusinessRules
{

    public async Task StockCardShouldExistWhenSelected(X.StockCard? item)
    {
        if (item == null)
            throw new BusinessException(StockCardsBusinessMessages.StockCardNotExists);
    }



}
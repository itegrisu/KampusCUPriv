using Application.Features.OfferManagementFeatures.Offers.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Rules;

public class OfferBusinessRules : BaseBusinessRules
{
    public async Task OfferShouldExistWhenSelected(X.Offer? item)
    {
        if (item == null)
            throw new BusinessException(OffersBusinessMessages.OfferNotExists);
    }
}
using Application.Features.OfferManagementFeatures.OfferTransactions.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.OfferManagements;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Rules;

public class OfferTransactionBusinessRules : BaseBusinessRules
{

    private readonly IOfferReadRepository _offerReadRepository;
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public OfferTransactionBusinessRules(IOfferReadRepository offerReadRepository, ICurrencyReadRepository currencyReadRepository)
    {
        _offerReadRepository = offerReadRepository;
        _currencyReadRepository = currencyReadRepository;
    }

    public async Task OfferTransactionShouldExistWhenSelected(X.OfferTransaction? item)
    {
        if (item == null)
            throw new BusinessException(OfferTransactionsBusinessMessages.OfferTransactionNotExists);
    }

    public async Task OfferShouldExistWhenSelected(Guid offerGid)
    {
        Offer offer = await _offerReadRepository.GetAsync(predicate: x => x.Gid == offerGid);

        if (offer == null)
            throw new BusinessException(OfferTransactionsBusinessMessages.OfferNotExists);
    }

    public async Task CurrencyShouldExistWhenSelected(Guid currencyGid)
    {
        Currency currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == currencyGid);

        if (currency == null)
            throw new BusinessException(OfferTransactionsBusinessMessages.CurrencyNotExists);
    }

}
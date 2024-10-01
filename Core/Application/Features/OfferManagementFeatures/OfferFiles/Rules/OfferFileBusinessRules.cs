using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Rules;

public class OfferFileBusinessRules : BaseBusinessRules
{
    //GidOfferFK
    private readonly IOfferReadRepository _offerReadRepository;

    public OfferFileBusinessRules(IOfferReadRepository offerReadRepository)
    {
        _offerReadRepository = offerReadRepository;
    }

    public async Task OfferFileShouldExistWhenSelected(X.OfferFile? item)
    {
        if (item == null)
            throw new BusinessException(OfferFilesBusinessMessages.OfferFileNotExists);
    }

    public async Task OfferShouldExistWhenSelected(Guid gidOfferFK)
    {
        X.Offer? offer = await _offerReadRepository.GetAsync(predicate: x => x.Gid == gidOfferFK);
        if (offer == null)
            throw new BusinessException(OfferFilesBusinessMessages.OfferNotExists);
    }

}
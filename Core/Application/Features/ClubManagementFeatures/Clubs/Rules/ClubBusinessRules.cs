using Application.Features.ClubFeatures.Clubs.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.Clubs.Rules;

public class ClubBusinessRules : BaseBusinessRules
{
    public async Task ClubShouldExistWhenSelected(X.Club? item)
    {
        if (item == null)
            throw new BusinessException(ClubsBusinessMessages.ClubNotExists);
    }
}
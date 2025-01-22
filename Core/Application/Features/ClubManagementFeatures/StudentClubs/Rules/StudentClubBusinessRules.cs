using Application.Features.ClubFeatures.StudentClubs.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.StudentClubs.Rules;

public class StudentClubBusinessRules : BaseBusinessRules
{
    public async Task StudentClubShouldExistWhenSelected(X.StudentClub? item)
    {
        if (item == null)
            throw new BusinessException(StudentClubsBusinessMessages.StudentClubNotExists);
    }
}
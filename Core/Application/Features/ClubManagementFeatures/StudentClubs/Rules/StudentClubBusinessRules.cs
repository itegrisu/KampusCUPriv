using Application.Features.ClubFeatures.StudentClubs.Constants;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.StudentClubs.Rules;

public class StudentClubBusinessRules : BaseBusinessRules
{
    private readonly IStudentClubReadRepository _studentClubReadRepository;

    public StudentClubBusinessRules(IStudentClubReadRepository studentClubReadRepository)
    {
        _studentClubReadRepository = studentClubReadRepository;
    }

    public async Task StudentClubShouldExistWhenSelected(X.StudentClub? item)
    {
        if (item == null)
            throw new BusinessException(StudentClubsBusinessMessages.StudentClubNotExists);
    }

    public async Task UserCannotJoinSameClubTwice(Guid userGid, Guid clubGid)
    {
        var existingStudentClub = await _studentClubReadRepository.GetAsync(sc => sc.GidUserFK == userGid && sc.GidClubFK == clubGid);
        if (existingStudentClub != null)
            throw new BusinessException(StudentClubsBusinessMessages.UserAlreadyInClub);
    }
}

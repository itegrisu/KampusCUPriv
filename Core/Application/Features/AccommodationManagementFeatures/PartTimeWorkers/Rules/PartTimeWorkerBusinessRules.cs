using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Constants;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;

public class PartTimeWorkerBusinessRules : BaseBusinessRules
{
    private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;

    public PartTimeWorkerBusinessRules(IPartTimeWorkerReadRepository partTimeWorkerReadRepository)
    {
        _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
    }

    public async Task PartTimeWorkerShouldExistWhenSelected(X.PartTimeWorker? item)
    {
        if (item == null)
            throw new BusinessException(PartTimeWorkersBusinessMessages.PartTimeWorkerNotExists);
    }

    public async Task IsUserNameUnique(string userNmae)
    {
        if (await _partTimeWorkerReadRepository.GetSingleAsync(x => x.UserName == userNmae) != null)
            throw new BusinessException(PartTimeWorkersBusinessMessages.UserNameAlreadyExist);
    }

}
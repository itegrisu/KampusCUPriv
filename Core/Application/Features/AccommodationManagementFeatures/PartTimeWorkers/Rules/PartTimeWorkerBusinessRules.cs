using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;

public class PartTimeWorkerBusinessRules : BaseBusinessRules
{
    public async Task PartTimeWorkerShouldExistWhenSelected(X.PartTimeWorker? item)
    {
        if (item == null)
            throw new BusinessException(PartTimeWorkersBusinessMessages.PartTimeWorkerNotExists);
    }
}
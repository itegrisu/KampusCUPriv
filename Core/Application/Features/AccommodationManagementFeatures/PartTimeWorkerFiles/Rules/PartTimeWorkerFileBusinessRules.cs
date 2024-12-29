using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Rules;

public class PartTimeWorkerFileBusinessRules : BaseBusinessRules
{
    public async Task PartTimeWorkerFileShouldExistWhenSelected(X.PartTimeWorkerFile? item)
    {
        if (item == null)
            throw new BusinessException(PartTimeWorkerFilesBusinessMessages.PartTimeWorkerFileNotExists);
    }
}
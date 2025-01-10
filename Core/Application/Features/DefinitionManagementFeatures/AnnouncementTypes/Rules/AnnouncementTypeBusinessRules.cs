using Application.Features.DefinitionFeatures.AnnouncementTypes.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Rules;

public class AnnouncementTypeBusinessRules : BaseBusinessRules
{
    public async Task AnnouncementTypeShouldExistWhenSelected(X.AnnouncementType? item)
    {
        if (item == null)
            throw new BusinessException(AnnouncementTypesBusinessMessages.AnnouncementTypeNotExists);
    }
}
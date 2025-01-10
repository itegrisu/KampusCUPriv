using Application.Features.DefinitionFeatures.Classes.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Classes.Rules;

public class ClassBusinessRules : BaseBusinessRules
{
    public async Task ClassShouldExistWhenSelected(X.Class? item)
    {
        if (item == null)
            throw new BusinessException(ClassesBusinessMessages.ClassNotExists);
    }
}
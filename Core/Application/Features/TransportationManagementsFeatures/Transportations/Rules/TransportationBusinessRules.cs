using Application.Features.TransportationManagementFeatures.Transportations.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Rules;

public class TransportationBusinessRules : BaseBusinessRules
{
    public async Task TransportationShouldExistWhenSelected(X.Transportation? item)
    {
        if (item == null)
            throw new BusinessException(TransportationsBusinessMessages.TransportationNotExists);
    }
}
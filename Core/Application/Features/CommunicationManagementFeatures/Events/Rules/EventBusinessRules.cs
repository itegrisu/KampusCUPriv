using Application.Features.CommunicationFeatures.Events.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Events.Rules;

public class EventBusinessRules : BaseBusinessRules
{
    public async Task EventShouldExistWhenSelected(X.Event? item)
    {
        if (item == null)
            throw new BusinessException(EventsBusinessMessages.EventNotExists);
    }
}
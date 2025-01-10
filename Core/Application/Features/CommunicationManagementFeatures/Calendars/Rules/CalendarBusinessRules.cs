using Application.Features.CommunicationFeatures.Calendars.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Calendars.Rules;

public class CalendarBusinessRules : BaseBusinessRules
{
    public async Task CalendarShouldExistWhenSelected(X.Calendar? item)
    {
        if (item == null)
            throw new BusinessException(CalendarsBusinessMessages.CalendarNotExists);
    }
}
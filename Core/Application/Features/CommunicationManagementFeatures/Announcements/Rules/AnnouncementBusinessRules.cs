using Application.Features.CommunicationFeatures.Announcements.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Announcements.Rules;

public class AnnouncementBusinessRules : BaseBusinessRules
{
    public async Task AnnouncementShouldExistWhenSelected(X.Announcement? item)
    {
        if (item == null)
            throw new BusinessException(AnnouncementsBusinessMessages.AnnouncementNotExists);
    }
}
using Application.Features.CommunicationFeatures.StudentAnnouncements.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;

public class StudentAnnouncementBusinessRules : BaseBusinessRules
{
    public async Task StudentAnnouncementShouldExistWhenSelected(X.StudentAnnouncement? item)
    {
        if (item == null)
            throw new BusinessException(StudentAnnouncementsBusinessMessages.StudentAnnouncementNotExists);
    }
}
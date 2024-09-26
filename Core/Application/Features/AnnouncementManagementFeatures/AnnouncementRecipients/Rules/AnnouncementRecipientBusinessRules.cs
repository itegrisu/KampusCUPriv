using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Constants;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.AnnouncementManagements;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Rules;

public class AnnouncementRecipientBusinessRules : BaseBusinessRules
{
    private readonly IAnnouncementReadRepository _announcementReadRepository;
    private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    public AnnouncementRecipientBusinessRules(IAnnouncementReadRepository announcementReadRepository, IAnnouncementRecipientReadRepository announcementRecipientReadRepository, IUserReadRepository userReadRepository)
    {
        _announcementReadRepository = announcementReadRepository;
        _announcementRecipientReadRepository = announcementRecipientReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task AnnouncementRecipientShouldExistWhenSelected(Guid gid)
    {
        if (_announcementRecipientReadRepository.GetByGidAsync(gid) == null)
            throw new BusinessException(AnnouncementRecipientsBusinessMessages.ErrorAnnouncementRecipientShouldExist);
    }

    public async Task AnnouncementShouldExistWhenSelected(Guid announcementGid)
    {
        if (_announcementReadRepository.GetByGidAsync(announcementGid) == null)
        {
            throw new BusinessException(AnnouncementRecipientsBusinessMessages.ErrorAnnouncementShouldExist);
        }
    }

    public async Task RecipientShouldExistWhenSelected(Guid userGuid)
    {
        if (_userReadRepository.GetByGidAsync(userGuid) == null)
        {
            throw new BusinessException(AnnouncementRecipientsBusinessMessages.UserNotExists);
        }
    }

}
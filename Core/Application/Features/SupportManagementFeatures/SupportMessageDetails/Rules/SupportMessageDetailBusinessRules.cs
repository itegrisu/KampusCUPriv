using Application.Features.SupportManagementFeatures.SupportMessageDetails.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Rules;

public class SupportMessageDetailBusinessRules : BaseBusinessRules
{
   

    public readonly IUserReadRepository _userReadRepository;
    public readonly ISupportRequestReadRepository _supportRequestReadRepository;

    public SupportMessageDetailBusinessRules(IUserReadRepository userReadRepository, ISupportRequestReadRepository supportRequestReadRepository)
    {
        _userReadRepository = userReadRepository;
        _supportRequestReadRepository = supportRequestReadRepository;
    }

    public async Task SupportMessageDetailShouldExistWhenSelected(X.SupportMessageDetail? item)
    {
        if (item == null)
            throw new BusinessException(SupportMessageDetailsBusinessMessages.SupportMessageDetailNotExists);
    }

    public async Task SupportRequestShouldExistWhenSelected(Guid gid)
    {
        if (await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == gid) == null)
            throw new BusinessException(SupportMessageDetailsBusinessMessages.SupportRequestNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gid)
    {
        if (await _userReadRepository.GetAsync(predicate: x => x.Gid == gid) == null)
            throw new BusinessException(SupportMessageDetailsBusinessMessages.UserNotExists);
    }

}
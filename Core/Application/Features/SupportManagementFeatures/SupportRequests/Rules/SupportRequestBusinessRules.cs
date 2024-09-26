using Application.Features.SupportManagementFeatures.SupportRequests.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Rules;

public class SupportRequestBusinessRules : BaseBusinessRules
{
    

    private readonly IUserReadRepository _userReadRepository;

    public SupportRequestBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task SupportRequestShouldExistWhenSelected(X.SupportRequest? item)
    {
        if (item == null)
            throw new BusinessException(SupportRequestsBusinessMessages.SupportRequestNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid SenderGid)
    {
        if (await _userReadRepository.GetAsync(predicate: x => x.Gid == SenderGid) == null)
            throw new BusinessException(SupportRequestsBusinessMessages.UserNotExists);
    }
}
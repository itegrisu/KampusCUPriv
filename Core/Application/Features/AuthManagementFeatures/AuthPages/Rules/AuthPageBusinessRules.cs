using Core.Application;
using Domain.Entities.AuthManagements;
using Core.CrossCuttingConcern.Exceptions;
using Application.Features.AuthManagementFeatures.AuthPages.Constants;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;

namespace Application.Features.AuthManagementFeatures.AuthPages.Rules;

public class AuthPageBusinessRules : BaseBusinessRules
{
    private readonly IAuthPageReadRepository _authPageReadRepository;


    public AuthPageBusinessRules(IAuthPageReadRepository authPageReadRepository)
    {
        _authPageReadRepository = authPageReadRepository;

    }

    public async Task AuthPageShouldExistWhenSelected(Guid gid)
    {
        if (await _authPageReadRepository.GetByGidAsync(gid) == null)
            throw new BusinessException(AuthPagesBusinessMessages.AuthPageNotExists);
    }
}
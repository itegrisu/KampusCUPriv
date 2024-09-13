using Application.Features.AuthManagementFeatures.AuthRolePages.Rules;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;

public class GetByIdAuthRolePageQuery : IRequest<GetByGidAuthRolePageResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAuthRolePageQueryHandler : IRequestHandler<GetByIdAuthRolePageQuery, GetByGidAuthRolePageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly AuthRolePageBusinessRules _authRolePageBusinessRules;

        public GetByIdAuthRolePageQueryHandler(IMapper mapper, IAuthRolePageReadRepository authRolePageReadRepository, AuthRolePageBusinessRules authRolePageBusinessRules)
        {
            _mapper = mapper;
            _authRolePageReadRepository = authRolePageReadRepository;
            _authRolePageBusinessRules = authRolePageBusinessRules;
        }

        public async Task<GetByGidAuthRolePageResponse> Handle(GetByIdAuthRolePageQuery request, CancellationToken cancellationToken)
        {
            await _authRolePageBusinessRules.AuthRolePageShouldExistWhenSelected(request.Gid); // Role Page'in olup olmadýðýný kontrol eder.
            AuthRolePage? authRolePage = await _authRolePageReadRepository.GetAsync(predicate: arp => arp.Gid == request.Gid,
                include: arp => arp.Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK),
                cancellationToken: cancellationToken);
          

            GetByGidAuthRolePageResponse response = _mapper.Map<GetByGidAuthRolePageResponse>(authRolePage);
            return response;
        }
    }
}
using Application.Features.AuthManagementFeatures.AuthRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;

public class GetByGidAuthRoleQuery : IRequest<GetByGidAuthRoleResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAuthRoleQueryHandler : IRequestHandler<GetByGidAuthRoleQuery, GetByGidAuthRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRoleReadRepository _authRoleReadRepository;
        private readonly AuthRoleBusinessRules _authRoleBusinessRules;

        public GetByIdAuthRoleQueryHandler(IMapper mapper, IAuthRoleReadRepository authRoleReadRepository, AuthRoleBusinessRules authRoleBusinessRules)
        {
            _mapper = mapper;
            _authRoleReadRepository = authRoleReadRepository;
            _authRoleBusinessRules = authRoleBusinessRules;
        }

        public async Task<GetByGidAuthRoleResponse> Handle(GetByGidAuthRoleQuery request, CancellationToken cancellationToken)
        {
            await _authRoleBusinessRules.AuthRoleShouldExistWhenSelected(request.Gid);
            AuthRole? authRole = await _authRoleReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);
            
            GetByGidAuthRoleResponse response = _mapper.Map<GetByGidAuthRoleResponse>(authRole);
            return response;
        }
    }
}
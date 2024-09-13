using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;

public class GetByGidAuthUserRoleQuery : IRequest<GetByGidAuthUserRoleResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAuthUserRoleQueryHandler : IRequestHandler<GetByGidAuthUserRoleQuery, GetByGidAuthUserRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;

        public GetByIdAuthUserRoleQueryHandler(IMapper mapper, IAuthUserRoleWriteRepository authUserRoleWriteRepository, IAuthUserRoleReadRepository authUserRoleReadRepository, AuthUserRoleBusinessRules authUserRoleBusinessRules)
        {
            _mapper = mapper;
            _authUserRoleWriteRepository = authUserRoleWriteRepository;
            _authUserRoleReadRepository = authUserRoleReadRepository;
            _authUserRoleBusinessRules = authUserRoleBusinessRules;
        }

        public async Task<GetByGidAuthUserRoleResponse> Handle(GetByGidAuthUserRoleQuery request, CancellationToken cancellationToken)
        {
            await _authUserRoleBusinessRules.AuthUserRoleShouldExistWhenSelected(request.Gid);
            AuthUserRole? authUserRole = await _authUserRoleReadRepository.GetAsync(predicate: aur => aur.Gid == request.Gid,
                include: aur => aur.Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK).Include(i => i.UserFK),
                cancellationToken: cancellationToken);

            GetByGidAuthUserRoleResponse response = _mapper.Map<GetByGidAuthUserRoleResponse>(authUserRole);
            return response;
        }
    }
}
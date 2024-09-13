using Application.Features.AuthManagementFeatures.AuthRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Create;

public class CreateAuthRoleCommand : IRequest<CreatedAuthRoleResponse>
{
    public string RoleName { get; set; }
    public string? RoleDescription { get; set; }
    public string? IconImage { get; set; }

    public class CreateAuthRoleCommandHandler : IRequestHandler<CreateAuthRoleCommand, CreatedAuthRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRoleWriteRepository _authRoleWriteRepository;
        private readonly IAuthRoleReadRepository _authRoleReadRepository;
        private readonly AuthRoleBusinessRules _authRoleBusinessRules;

        public CreateAuthRoleCommandHandler(IMapper mapper, IAuthRoleWriteRepository authRoleWriteRepository,
                                         AuthRoleBusinessRules authRoleBusinessRules, IAuthRoleReadRepository authRoleReadRepository)
        {
            _mapper = mapper;
            _authRoleWriteRepository = authRoleWriteRepository;
            _authRoleBusinessRules = authRoleBusinessRules;
            _authRoleReadRepository = authRoleReadRepository;
        }

        public async Task<CreatedAuthRoleResponse> Handle(CreateAuthRoleCommand request, CancellationToken cancellationToken)
        {
            List<AuthRole> roles = await _authRoleReadRepository.GetAll().ToListAsync();
            int maxRowNo = 0;
            if (roles.Count > 0)
            {
                maxRowNo = await _authRoleReadRepository.GetAll().MaxAsync(r => r.RowNo);
            }

            AuthRole authRole = _mapper.Map<AuthRole>(request);
            authRole.RowNo = maxRowNo + 1;

            await _authRoleWriteRepository.AddAsync(authRole);
            await _authRoleWriteRepository.SaveAsync();

            GetByGidAuthRoleResponse obj = _mapper.Map<GetByGidAuthRoleResponse>(authRole);

            return new()
            {
                Title = AuthRolesBusinessMessages.ProcessCompleted,
                IsValid = true,
                Message = AuthRolesBusinessMessages.SuccessCreatedAuthRoleMessage,
                Obj = obj
            };
        }
    }
}
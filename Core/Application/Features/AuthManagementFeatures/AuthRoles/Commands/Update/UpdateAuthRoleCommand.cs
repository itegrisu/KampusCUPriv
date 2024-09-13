using Application.Features.AuthManagementFeatures.AuthRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Update;

public class UpdateAuthRoleCommand : IRequest<UpdatedAuthRoleResponse>
{
    public Guid Gid { get; set; }
    public string RoleName { get; set; }
    public string? RoleDescription { get; set; }
    public string? IconImage { get; set; }

    public class UpdateAuthRoleCommandHandler : IRequestHandler<UpdateAuthRoleCommand, UpdatedAuthRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRoleReadRepository _authRoleReadRepository;
        private readonly IAuthRoleWriteRepository _authRoleWriteRepository;
        private readonly AuthRoleBusinessRules _authRoleBusinessRules;

        public UpdateAuthRoleCommandHandler(IMapper mapper, IAuthRoleReadRepository authRoleReadRepository,
                                         AuthRoleBusinessRules authRoleBusinessRules, IAuthRoleWriteRepository authRoleWriteRepository)
        {
            _mapper = mapper;
            _authRoleReadRepository = authRoleReadRepository;
            _authRoleBusinessRules = authRoleBusinessRules;
            _authRoleWriteRepository = authRoleWriteRepository;
        }

        public async Task<UpdatedAuthRoleResponse> Handle(UpdateAuthRoleCommand request, CancellationToken cancellationToken)
        {
            await _authRoleBusinessRules.AuthRoleShouldExistWhenSelected(request.Gid);
            AuthRole? authRole = await _authRoleReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);
            authRole = _mapper.Map(request, authRole);

            _authRoleWriteRepository.Update(authRole!);
            await _authRoleWriteRepository.SaveAsync();

            GetByGidAuthRoleResponse obj = _mapper.Map<GetByGidAuthRoleResponse>(authRole);


            return new()
            {
                Title = AuthRolesBusinessMessages.ProcessCompleted,
                IsValid = true,
                Message = AuthRolesBusinessMessages.SuccessUpdatedAuthRoleMessage,
                Obj = obj
            };
        }
    }
}
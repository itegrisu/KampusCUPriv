using Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Delete;

public class DeleteAuthUserRoleCommand : IRequest<DeletedAuthUserRoleResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAuthUserRoleCommandHandler : IRequestHandler<DeleteAuthUserRoleCommand, DeletedAuthUserRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;

        public DeleteAuthUserRoleCommandHandler(IMapper mapper, IAuthUserRoleWriteRepository authUserRoleWriteRepository, IAuthUserRoleReadRepository authUserRoleReadRepository, AuthUserRoleBusinessRules authUserRoleBusinessRules)
        {
            _mapper = mapper;
            _authUserRoleWriteRepository = authUserRoleWriteRepository;
            _authUserRoleReadRepository = authUserRoleReadRepository;
            _authUserRoleBusinessRules = authUserRoleBusinessRules;
        }

        public async Task<DeletedAuthUserRoleResponse> Handle(DeleteAuthUserRoleCommand request, CancellationToken cancellationToken)
        {
            await _authUserRoleBusinessRules.AuthUserRoleShouldExistWhenSelected(request.Gid);
            AuthUserRole? authUserRole = await _authUserRoleReadRepository.GetAsync(predicate: aur => aur.Gid == request.Gid, cancellationToken: cancellationToken);

            authUserRole.DataState = Core.Enum.DataState.Deleted;
            _authUserRoleWriteRepository.Update(authUserRole);
            await _authUserRoleWriteRepository.SaveAsync();

            return new()
            {
                Title = AuthUserRolesBusinessMessages.ProcessCompleted,
                Message = AuthUserRolesBusinessMessages.SuccessDeletedAuthUserRoleMessage,
                IsValid = true
            };
        }
    }
}
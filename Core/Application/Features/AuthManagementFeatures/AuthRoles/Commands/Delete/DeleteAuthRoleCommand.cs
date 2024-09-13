using Application.Features.AuthManagementFeatures.AuthRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Delete;

public class DeleteAuthRoleCommand : IRequest<DeletedAuthRoleResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAuthRoleCommandHandler : IRequestHandler<DeleteAuthRoleCommand, DeletedAuthRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRoleReadRepository _authRoleReadRepository;
        private readonly IAuthRoleWriteRepository _authRoleWriteRepository;
        private readonly AuthRoleBusinessRules _authRoleBusinessRules;

        public DeleteAuthRoleCommandHandler(IMapper mapper, IAuthRoleReadRepository authRoleReadRepository,
                                         AuthRoleBusinessRules authRoleBusinessRules, IAuthRoleWriteRepository authRoleWriteRepository)
        {
            _mapper = mapper;
            _authRoleReadRepository = authRoleReadRepository;
            _authRoleBusinessRules = authRoleBusinessRules;
            _authRoleWriteRepository = authRoleWriteRepository;
        }

        public async Task<DeletedAuthRoleResponse> Handle(DeleteAuthRoleCommand request, CancellationToken cancellationToken)
        {
            await _authRoleBusinessRules.AuthRoleShouldExistWhenSelected(request.Gid);
            AuthRole? authRole = await _authRoleReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);
        
            authRole.DataState = Core.Enum.DataState.Deleted;
            _authRoleWriteRepository.Update(authRole!);
            await _authRoleWriteRepository.SaveAsync();


            return new()
            {
                Title = AuthRolesBusinessMessages.ProcessCompleted,
                IsValid = true,
                Message = AuthRolesBusinessMessages.SuccessDeletedAuthRoleMessage
            };
        }
    }
}
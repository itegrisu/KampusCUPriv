using Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Update;

public class UpdateAuthUserRoleCommand : IRequest<UpdatedAuthUserRoleResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }
    public int RowNo { get; set; }

    public class UpdateAuthUserRoleCommandHandler : IRequestHandler<UpdateAuthUserRoleCommand, UpdatedAuthUserRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;
        private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;

        public UpdateAuthUserRoleCommandHandler(IMapper mapper, AuthUserRoleBusinessRules authUserRoleBusinessRules, IAuthUserRoleWriteRepository authUserRoleWriteRepository, IAuthUserRoleReadRepository authUserRoleReadRepository)
        {
            _mapper = mapper;
            _authUserRoleBusinessRules = authUserRoleBusinessRules;
            _authUserRoleWriteRepository = authUserRoleWriteRepository;
            _authUserRoleReadRepository = authUserRoleReadRepository;
        }

        public async Task<UpdatedAuthUserRoleResponse> Handle(UpdateAuthUserRoleCommand request, CancellationToken cancellationToken)
        {

            await _authUserRoleBusinessRules.AuthUserRoleShouldExistWhenSelected(request.Gid);
            await _authUserRoleBusinessRules.PageShouldExistWhenSelected(request.GidPageFK);
            await _authUserRoleBusinessRules.RoleShouldExistWhenSelected(request.GidRoleFK);
            AuthUserRole? authUserRole = await _authUserRoleReadRepository.GetAsync(predicate: aur => aur.Gid == request.Gid, cancellationToken: cancellationToken);
            authUserRole = _mapper.Map(request, authUserRole);

            _authUserRoleWriteRepository.Update(authUserRole!);
            await _authUserRoleWriteRepository.SaveAsync();

            AuthUserRole? updateAuthUserRole = await _authUserRoleReadRepository.GetAsync(
               predicate: aur => aur.Gid == authUserRole.Gid, cancellationToken: cancellationToken,
               include: source => source.Include(aur => aur.AuthRoleFK).Include(aur => aur.AuthPageFK).Include(aur => aur.UserFK)
               );

            GetByGidAuthUserRoleResponse obj = _mapper.Map<GetByGidAuthUserRoleResponse>(updateAuthUserRole);

            return new()
            {
                Title = AuthUserRolesBusinessMessages.ProcessCompleted,
                Message = AuthUserRolesBusinessMessages.SuccessUpdatedAuthUserRoleMessage,
                IsValid = true

            };
        }
    }
}
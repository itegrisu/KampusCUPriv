using Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Create;

public class CreateAuthUserRoleCommand : IRequest<CreatedAuthUserRoleResponse>
{
    public Guid GidUserFK { get; set; }
    public Guid? GidRoleFK { get; set; }
    public Guid? GidPageFK { get; set; }
    public bool? comeUserLevelPage { get; set; }
  
    public class CreateAuthUserRoleCommandHandler : IRequestHandler<CreateAuthUserRoleCommand, CreatedAuthUserRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;

        public CreateAuthUserRoleCommandHandler(IMapper mapper, IAuthUserRoleWriteRepository authUserRoleWriteRepository, AuthUserRoleBusinessRules authUserRoleBusinessRules, IAuthUserRoleReadRepository authUserRoleReadRepository)
        {
            _mapper = mapper;
            _authUserRoleWriteRepository = authUserRoleWriteRepository;
            _authUserRoleBusinessRules = authUserRoleBusinessRules;
            _authUserRoleReadRepository = authUserRoleReadRepository;
        }

        public async Task<CreatedAuthUserRoleResponse> Handle(CreateAuthUserRoleCommand request, CancellationToken cancellationToken)
        {
            
            await _authUserRoleBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            await _authUserRoleBusinessRules.PageShouldExistWhenSelected(request.GidPageFK);
            await _authUserRoleBusinessRules.RoleShouldExistWhenSelected(request.GidRoleFK);

            if (request.comeUserLevelPage != true)
            {
            await _authUserRoleBusinessRules.AuthRoleHasBeenAddedBefore(request.GidRoleFK, request.GidUserFK);
            }
            
            await _authUserRoleBusinessRules.AuthPageHasBeenAddedBefore(request.GidPageFK, request.GidUserFK);
            await _authUserRoleBusinessRules.DoesUserHasPage(request.GidUserFK, request.GidPageFK);
            

            List<AuthUserRole> authUserRoles = await _authUserRoleReadRepository.GetAll().Where(x => x.GidUserFK == request.GidUserFK).ToListAsync();
            int maxRowNo = 0;
            if (authUserRoles.Count > 0)
            {
                maxRowNo = await _authUserRoleReadRepository.GetAll().MaxAsync(r => r.RowNo);
            }

            AuthUserRole authUserRole = _mapper.Map<AuthUserRole>(request);
            authUserRole.RowNo = maxRowNo + 1;

            await _authUserRoleWriteRepository.AddAsync(authUserRole);
            await _authUserRoleWriteRepository.SaveAsync();

            AuthUserRole? savedAuthUserRole = await _authUserRoleReadRepository.GetAsync(
                predicate: aur => aur.Gid == authUserRole.Gid, cancellationToken: cancellationToken,
                include: source => source.Include(aur => aur.AuthRoleFK).Include(aur => aur.AuthPageFK).Include(aur => aur.UserFK)
                );

            GetByGidAuthUserRoleResponse obj = _mapper.Map<GetByGidAuthUserRoleResponse>(savedAuthUserRole);

            return new()
            {
                Title = AuthUserRolesBusinessMessages.SectionName,
                Message = AuthUserRolesBusinessMessages.SuccessCreatedAuthUserRoleMessage,
                IsValid = true,
                Obj = obj,
            };
        }
    }
}
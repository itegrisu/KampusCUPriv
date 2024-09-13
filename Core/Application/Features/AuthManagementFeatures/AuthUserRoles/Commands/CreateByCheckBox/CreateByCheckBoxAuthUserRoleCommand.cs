using Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.CreateByCheckBox
{
    public class CreateByCheckBoxAuthUserRoleCommand : IRequest<CreatedByCheckBoxAuthUserRoleResponse>
    {
        public Guid GidUserFK { get; set; }
        public Guid GidRoleFK { get; set; }

        public class CreateByCheckBoxAuthUserRoleCommandHandler : IRequestHandler<CreateByCheckBoxAuthUserRoleCommand, CreatedByCheckBoxAuthUserRoleResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthUserRoleWriteRepository _authUserRoleWriteRepository;
            private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
            private readonly AuthUserRoleBusinessRules _authUserRoleBusinessRules;

            public CreateByCheckBoxAuthUserRoleCommandHandler(IMapper mapper, IAuthUserRoleWriteRepository authUserRoleWriteRepository, IAuthUserRoleReadRepository authUserRoleReadRepository, AuthUserRoleBusinessRules authUserRoleBusinessRules)
            {
                _mapper = mapper;
                _authUserRoleWriteRepository = authUserRoleWriteRepository;
                _authUserRoleReadRepository = authUserRoleReadRepository;
                _authUserRoleBusinessRules = authUserRoleBusinessRules;
            }

            public async Task<CreatedByCheckBoxAuthUserRoleResponse> Handle(CreateByCheckBoxAuthUserRoleCommand request, CancellationToken cancellationToken)
            {
                await _authUserRoleBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
                await _authUserRoleBusinessRules.RoleShouldExistWhenSelected(request.GidRoleFK);


                if (await _authUserRoleBusinessRules.AuthRoleHasBeenAddedBeforeByCheckBox(request.GidRoleFK, request.GidUserFK) == true)
                {//Burada rolü siliyoruz
                    AuthUserRole? deletedAuthUserRole = await _authUserRoleReadRepository.GetAsync(predicate: aur => aur.GidRoleFK == request.GidRoleFK, cancellationToken: cancellationToken);

                    deletedAuthUserRole.DataState = Core.Enum.DataState.Deleted;
                    _authUserRoleWriteRepository.Update(deletedAuthUserRole);
                    await _authUserRoleWriteRepository.SaveAsync();

                    return new()
                    {
                        Title = AuthUserRolesBusinessMessages.SectionName,
                        Message = AuthUserRolesBusinessMessages.SuccessDeletedAuthUserRoleMessage,
                        IsValid = true,
                        ActionType="Delete"
                    };

                }

                List<AuthUserRole> authUserRoles = await _authUserRoleReadRepository.GetAll().ToListAsync();
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
}

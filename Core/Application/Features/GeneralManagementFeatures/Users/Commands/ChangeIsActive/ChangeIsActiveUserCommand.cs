using Application.Features.GeneralManagementFeatures.Users.Commands.Delete;
using Application.Features.GeneralManagementFeatures.Users.Commands.Update;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.ChangeIsActive
{
    public class ChangeIsActiveUserCommand : IRequest<ChangeIsActiveUserResponse>
    {
        public Guid Gid { get; set; }


        public class ChangeIsActiveUserCommandHandler : IRequestHandler<ChangeIsActiveUserCommand, ChangeIsActiveUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly UserBusinessRules _userCustomBusinessRules;

            public ChangeIsActiveUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                             UserBusinessRules userCustomBusinessRules, IUserWriteRepository userWriteRepository)
            {
                _mapper = mapper;
                _userReadRepository = userReadRepository;
                _userCustomBusinessRules = userCustomBusinessRules;
                _userWriteRepository = userWriteRepository;
            }

            public async Task<ChangeIsActiveUserResponse> Handle(ChangeIsActiveUserCommand request, CancellationToken cancellationToken)
            {
                await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);

                User? user = await _userReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                user.IsActive = false;
                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();

                var deletedUser = await _userReadRepository.GetListAllAsync(predicate: u => u.Gid == request.Gid,
                    include: u => u.Include(u => u.CountryFK));

                GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(deletedUser.Items[0]);

                return new()
                {
                    Obj = obj,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.SuccessDeletedUserMessage,
                    IsValid = true
                };
            }
        }
    }
}

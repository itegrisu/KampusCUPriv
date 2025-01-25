using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                             UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _userReadRepository = userReadRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userReadRepository.GetAsync(predicate: x => x.Email == request.Email && x.Password == request.Password, cancellationToken: cancellationToken);

                if (user == null)
                {
                    return new()
                    {
                        Title = UsersBusinessMessages.NotFoundRecord,
                        Message = UsersBusinessMessages.UserNotExists,
                        IsValid = false
                    };
                }

                return new()
                {
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = UsersBusinessMessages.SectionName,
                    IsValid = true
                };
            }
        }
    }
}

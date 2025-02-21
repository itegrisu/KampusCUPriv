using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Features.GeneralFeatures.Admins.Rules;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Admins.Commands.Login
{
    public class LoginAdminCommand : IRequest<LoginAdminResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginUserCommandHandler : IRequestHandler<LoginAdminCommand, LoginAdminResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAdminReadRepository _adminReadRepository;
            private readonly AdminBusinessRules _adminBusinessRules;
            public LoginUserCommandHandler(IMapper mapper, IAdminReadRepository adminReadRepository, AdminBusinessRules adminBusinessRules)
            {
                _mapper = mapper;
                _adminReadRepository = adminReadRepository;
                _adminBusinessRules = adminBusinessRules;
            }

            public async Task<LoginAdminResponse> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
            {
                Admin? admin = await _adminReadRepository.GetAsync(predicate: x => x.Email == request.Email && x.Password == request.Password, cancellationToken: cancellationToken);

                if (admin == null)
                {
                    return new()
                    {
                        Title = AdminsBusinessMessages.NotFoundRecord,
                        Message = AdminsBusinessMessages.AdminNotExists,
                        IsValid = false
                    };
                }

                return new()
                {
                    Title = AdminsBusinessMessages.ProcessCompleted,
                    Message = AdminsBusinessMessages.SectionName,
                    IsValid = true,
                    ClubGid = admin.Gid
                };
            }
        }
    }
}

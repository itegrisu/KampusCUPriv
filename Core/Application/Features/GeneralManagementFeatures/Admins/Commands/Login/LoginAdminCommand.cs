using Application.Abstractions.Token;
using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Features.GeneralFeatures.Admins.Rules;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Helpers;
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
            private readonly IAdminWriteRepository _adminWriteRepository;
            private readonly AdminBusinessRules _adminBusinessRules;
            private readonly ITokenHandler _tokenHandler;
            public LoginUserCommandHandler(IMapper mapper, IAdminReadRepository adminReadRepository, AdminBusinessRules adminBusinessRules, IAdminWriteRepository adminWriteRepository, ITokenHandler tokenHandler)
            {
                _mapper = mapper;
                _adminReadRepository = adminReadRepository;
                _adminBusinessRules = adminBusinessRules;
                _adminWriteRepository = adminWriteRepository;
                _tokenHandler = tokenHandler;
            }

            public async Task<LoginAdminResponse> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
            {
                Admin? admin = await _adminReadRepository.GetAsync(predicate: x => x.Email == request.Email, cancellationToken: cancellationToken);

                if (admin == null)
                {
                    return new()
                    {
                        Title = AdminsBusinessMessages.NotFoundRecord,
                        Message = AdminsBusinessMessages.AdminNotExists,
                        IsValid = false
                    };
                }

                bool isPasswordValid = HashingHelperForApplicationLayer.VeriFyPasswordHash(
                    request.Password,
                    admin.Password,
                    admin.PasswordSalt);

                if (!isPasswordValid)
                {
                    return new()
                    {
                        Title = "İşlem Başarısız",
                        Message = "Şifre hatalı",
                        IsValid = false
                    };
                }

                var token = _tokenHandler.CreateAccessToken(admin);

                admin.RefreshToken = token.RefreshToken;
                admin.RefreshTokenExpiration = token.RefreshTokenExpiration;

                _adminWriteRepository.Update(admin);
                await _adminWriteRepository.SaveAsync();

                return new()
                {
                    Title = AdminsBusinessMessages.ProcessCompleted,
                    Message = AdminsBusinessMessages.SectionName,
                    IsValid = true,
                    ClubGid = admin.GidClubFK,
                    Token = token.AccessToken,
                    RefreshToken = token.RefreshToken,
                };
            }
        }
    }
}

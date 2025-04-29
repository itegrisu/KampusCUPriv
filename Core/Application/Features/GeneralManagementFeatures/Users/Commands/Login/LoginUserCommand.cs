using Application.Abstractions.Token;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Helpers;
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
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly ITokenHandler _tokenHandler;
            public LoginUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                             UserBusinessRules userBusinessRules, ITokenHandler tokenHandler, IUserWriteRepository userWriteRepository)
            {
                _mapper = mapper;
                _userReadRepository = userReadRepository;
                _userBusinessRules = userBusinessRules;
                _tokenHandler = tokenHandler;
                _userWriteRepository = userWriteRepository;
            }

            // Application/Features/GeneralManagementFeatures/Users/Commands/Login/LoginUserCommand.cs
            public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userReadRepository.GetAsync(
                    predicate: x => x.Email == request.Email,
                    cancellationToken: cancellationToken);

                if (user == null)
                {
                    return new()
                    {
                        Title = UsersBusinessMessages.NotFoundRecord,
                        Message = "Email adresine sahip kullanıcı bulunamadı",
                        IsValid = false
                    };
                }

                if (!user.IsEmailVerified)
                {
                    return new()
                    {
                        Title = "İşlem Başarısız",
                        Message = "Email adresiniz henüz doğrulanmamış",
                        IsValid = false
                    };
                }

                bool isPasswordValid = HashingHelperForApplicationLayer.VeriFyPasswordHash(
                    request.Password,
                    user.Password, 
                    user.PasswordSalt);

                if (!isPasswordValid)
                {
                    return new()
                    {
                        Title = "İşlem Başarısız",
                        Message = "Şifre hatalı",
                        IsValid = false
                    };
                }

                var token = _tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiration = token.RefreshTokenExpiration;

                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();

                return new()
                {
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = "Giriş başarılı",
                    IsValid = true,
                    UserGid = user.Gid,
                    Token = token.AccessToken,
                    RefreshToken = token.RefreshToken
                };
            }
        }
    }
}

using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.VerifyEmail
{
    public class VerifyEmailUserCommand : IRequest<VerifyEmailUserResponse>
    {
        public string Email { get; set; }
        public string VerificationCode  { get; set; }
        public class VerifyEmailUserCommandHandler : IRequestHandler<VerifyEmailUserCommand, VerifyEmailUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IUserWriteRepository _userWriteRepository;
            public VerifyEmailUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                             UserBusinessRules userBusinessRules, IUserWriteRepository userWriteRepository)
            {
                _mapper = mapper;
                _userReadRepository = userReadRepository;
                _userBusinessRules = userBusinessRules;
                _userWriteRepository = userWriteRepository;
            }

            public async Task<VerifyEmailUserResponse> Handle(VerifyEmailUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userReadRepository.GetAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
                if (user == null)
                {
                    return new()
                    {
                        Title = UsersBusinessMessages.NotFoundRecord,
                        Message = "Kullanıcı bulunamadı.",
                        IsValid = false
                    };
                }

                // Kodun doğruluğunu ve süresinin geçmemiş olmasını kontrol edin
                if (user.EmailVerificationCode != request.VerificationCode || user.EmailVerificationCodeExpire < DateTime.UtcNow)
                {
                    return new()
                    {
                        Title = UsersBusinessMessages.InvalidCode,
                        Message = "Doğrulama kodu geçersiz veya süresi dolmuş.",
                        IsValid = false
                    };
                }

                // Doğrulama başarılı, kullanıcının email durumunu güncelleyin
                user.IsEmailVerified = true;
                // Artık kodu temizleyebilirsiniz
                user.EmailVerificationCode = null;
                user.EmailVerificationCodeExpire = null;

                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();

                return new()
                {
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = "E-posta doğrulaması başarılı.",
                    IsValid = true
                };
            }
        }
    }

}

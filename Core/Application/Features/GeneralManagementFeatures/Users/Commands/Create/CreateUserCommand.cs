using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Helpers;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public Guid GidDepartmentFK { get; set; }
    public Guid GidClassFK { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? IsBloodDonor { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsNotificationsEnabled { get; set; }


    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IEmailService _emailService;
        public CreateUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository,
                                         UserBusinessRules userBusinessRules, IUserReadRepository userReadRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _userWriteRepository = userWriteRepository;
            _userBusinessRules = userBusinessRules;
            _userReadRepository = userReadRepository;
            _emailService = emailService;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserAlreadyExist(request.Email);
            await _userBusinessRules.EmailDomainCheck(request.Email);

            //int maxRowNo = await _userReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.User user = _mapper.Map<X.User>(request);
            //user.RowNo = maxRowNo + 1;

            // Þifre Hashleme Ýþlemi
            string passwordHash, passwordSalt;
            HashingHelperForApplicationLayer.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            // Hash ve salt deðerlerini kullanýcý nesnesine kaydet
            user.Password = passwordHash;  // Düz metin þifre yerine hash deðerini sakla
            user.PasswordSalt = passwordSalt;

            // Kullanýcý kaydýnýn yapýldýðý anda doðrulama kodunu üretelim
            string verificationCode = GenerateVerificationCode();
            user.EmailVerificationCode = verificationCode;
            user.EmailVerificationCodeExpire = DateTime.UtcNow.AddHours(1); // Kodun 1 saat geçerli olduðunu varsayalým

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync();

            // Email gönderim servisini çaðýrýn (aþaðýda örnek vereceðiz)
            await _emailService.SendEmailAsync(user.Email, "Kayýt Doðrulama Kodu",
                $"Merhaba {user.Name},\n\nDoðrulama kodunuz: {verificationCode}\n\nKod 1 saat geçerlidir.");

            X.User savedUser = await _userReadRepository.GetAsync(predicate: x => x.Gid == user.Gid, include: x => x.Include(x => x.ClassFK).Include(x => x.DepartmentFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(savedUser);
            return new()
            {
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessCreatedUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }
    }
 

}


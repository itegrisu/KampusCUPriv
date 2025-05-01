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

            // �ifre Hashleme ��lemi
            string passwordHash, passwordSalt;
            HashingHelperForApplicationLayer.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            // Hash ve salt de�erlerini kullan�c� nesnesine kaydet
            user.Password = passwordHash;  // D�z metin �ifre yerine hash de�erini sakla
            user.PasswordSalt = passwordSalt;

            // Kullan�c� kayd�n�n yap�ld��� anda do�rulama kodunu �retelim
            string verificationCode = GenerateVerificationCode();
            user.EmailVerificationCode = verificationCode;
            user.EmailVerificationCodeExpire = DateTime.UtcNow.AddHours(1); // Kodun 1 saat ge�erli oldu�unu varsayal�m

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync();

            // Email g�nderim servisini �a��r�n (a�a��da �rnek verece�iz)
            await _emailService.SendEmailAsync(user.Email, "Kay�t Do�rulama Kodu",
                $"Merhaba {user.Name},\n\nDo�rulama kodunuz: {verificationCode}\n\nKod 1 saat ge�erlidir.");

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


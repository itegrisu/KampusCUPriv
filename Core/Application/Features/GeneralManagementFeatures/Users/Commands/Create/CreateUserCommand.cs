using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public Guid? GidNationalityFK { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string? Title { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }
    public string SifreGuncellemeToken { get; set; }
    public DateTime? TokenExpiredDate { get; set; }
    public string? Avatar { get; set; }
    public bool IsLoginStatus { get; set; }
    public bool IsSystemAdmin { get; set; }
    public string Gsm { get; set; }
    public string? Birthplace { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? IdentityNo { get; set; }
    public string? PassportNo { get; set; }
    public string? SGKNo { get; set; }
    public string? DrivingLicenseNo { get; set; }
    public string? Note { get; set; }
    public EnumMaritalStatus? MaritalStatus { get; set; }
    public EnumBloodGroup? BloodGroup { get; set; }
    public EnumGender Gender { get; set; }
    public EnumWorkType WorkType { get; set; }
    public EnumEmailActivationStatus EmailActivationStatus { get; set; }
    public EnumSmsActivationStatus SmsActivationStatus { get; set; }
    public string? PersonnelSpecialNote { get; set; }
    public bool IsActive { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly UserBusinessRules _userCustomBusinessRules;
        public CreateUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository,
                                         UserBusinessRules userCustomBusinessRules, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _userWriteRepository = userWriteRepository;
            _userCustomBusinessRules = userCustomBusinessRules;
            _userReadRepository = userReadRepository;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(request.IdentityNo != null)
            {
            await _userCustomBusinessRules.IdNumberAlreadyExists(request.IdentityNo);
            }

            await _userCustomBusinessRules.PhoneNumberAlreadyExists(request.Gsm);

            User userCustom = _mapper.Map<User>(request);
            userCustom.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Name.Trim().ToLower());
            userCustom.Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Surname.Trim().ToLower());

            await _userWriteRepository.AddAsync(userCustom);
            await _userWriteRepository.SaveAsync();

            User user = await _userReadRepository.GetAsync(predicate: u => u.Gid == userCustom.Gid,
                include: u => u.Include(x => x.CountryFK));

            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(user);

            return new()
            {
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessCreatedUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
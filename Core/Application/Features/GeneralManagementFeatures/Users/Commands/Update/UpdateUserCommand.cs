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

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public Guid Gid { get; set; }
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


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly UserBusinessRules _userCustomBusinessRules;

        public UpdateUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository,
                                         UserBusinessRules userCustomBusinessRules, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _userWriteRepository = userWriteRepository;
            _userCustomBusinessRules = userCustomBusinessRules;
            _userReadRepository = userReadRepository;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);

            var users = await _userReadRepository.GetListAllAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                include: u => u.Include(u => u.CountryFK));

            User user = users.Items[0];

            bool result = await _userCustomBusinessRules.UpdatedIdNumberAlreadyExists(request.IdentityNo, request.Gid.ToString(), user);
            if (!result)
            {
                return new UpdatedUserResponse
                {
                    Title = UsersBusinessMessages.TechnicalError,
                    Message = UsersBusinessMessages.IdNumberAlreadyExists,
                    IsValid = false,
                };
            }

            bool resultPhone = await _userCustomBusinessRules.UpdatedPhoneNumberAlreadyExists(request.Gsm, request.Gid.ToString(), user);
            if (!result)
            {
                return new UpdatedUserResponse
                {
                    Title = UsersBusinessMessages.TechnicalError,
                    Message = UsersBusinessMessages.PhoneNumberAlreadyExists,
                    IsValid = false,
                };
            }


            user = _mapper.Map(request, user);


            user.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Name.Trim().ToLower());
            user.Surname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Surname.Trim().ToLower());


            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(user);
            _userWriteRepository.Update(user!);
            await _userWriteRepository.SaveAsync();

            return new()
            {
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessUpdatedUserMessage,
                IsValid = true,
                Obj = obj

            };
        }



    }
}
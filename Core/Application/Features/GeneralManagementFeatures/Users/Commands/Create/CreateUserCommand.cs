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
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public string EPosta { get; set; }
    public string? Unvani { get; set; }
    public string SifreGuncellemeToken { get; set; }
    public DateTime? TokenGecerlilikSuresi { get; set; }
    public string? ProfilResmi { get; set; }
    public bool AktifHesapMi { get; set; }
    public bool SistemAdminMi { get; set; }
    public string Gsm { get; set; }
    public string? DogumYeri { get; set; }
    public DateTime? DogumTarihi { get; set; }
    public string? KimlikNo { get; set; }
    public string? PasaportNo { get; set; }
    public string? SGKNo { get; set; }
    public string? EhliyetNo { get; set; }
    public string? Not { get; set; }
    public EnumMedeniDurumu? MedeniDurumu { get; set; }
    public EnumKanGrubu? KanGrubu { get; set; }
    public EnumCinsiyet Cinsiyet { get; set; }
    public EnumEMailAktivasyonDurumu EMailAktivasyonDurumu { get; set; }
    public EnumSmsAktivasyonDurumu SmsAktivasyonDurumu { get; set; }

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
            await _userCustomBusinessRules.IdNumberAlreadyExists(request.KimlikNo);
            await _userCustomBusinessRules.PhoneNumberAlreadyExists(request.Gsm);

            User userCustom = _mapper.Map<User>(request);
            userCustom.Adi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Adi.Trim().ToLower());
            userCustom.Soyadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Soyadi.Trim().ToLower());

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
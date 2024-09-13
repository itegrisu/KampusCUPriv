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

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UpdateForAdmin
{
    public class UpdateForAdminUserCommand : IRequest<UpdateForAdminUserResponse>
    {
        public Guid Gid { get; set; }
        public Guid GidUyrukFK { get; set; }
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

        public class UpdateForAdminUserCommandHandler : IRequestHandler<UpdateForAdminUserCommand, UpdateForAdminUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserWriteRepository _userWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userCustomBusinessRules;

            public UpdateForAdminUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, UserBusinessRules userCustomBusinessRules)
            {
                _mapper = mapper;
                _userWriteRepository = userWriteRepository;
                _userReadRepository = userReadRepository;
                _userCustomBusinessRules = userCustomBusinessRules;
            }

            public async Task<UpdateForAdminUserResponse> Handle(UpdateForAdminUserCommand request, CancellationToken cancellationToken)
            {
                await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);

                User? user = await _userReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: u => u.Include(u => u.CountryFK));

                user = _mapper.Map(request, user);

                user.Adi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Adi.Trim().ToLower());
                user.Soyadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.Soyadi.Trim().ToLower());

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
}

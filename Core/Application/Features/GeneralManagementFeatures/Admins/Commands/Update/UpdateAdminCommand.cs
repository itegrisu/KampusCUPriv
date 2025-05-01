using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Application.Features.GeneralFeatures.Admins.Rules;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Application.Helpers;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Admins.Commands.Update;

public class UpdateAdminCommand : IRequest<UpdatedAdminResponse>
{
    public Guid Gid { get; set; }
    public Guid GidClubFK { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, UpdatedAdminResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminWriteRepository _adminWriteRepository;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly AdminBusinessRules _adminBusinessRules;

        public UpdateAdminCommandHandler(IMapper mapper, IAdminWriteRepository adminWriteRepository,
                                         AdminBusinessRules adminBusinessRules, IAdminReadRepository adminReadRepository)
        {
            _mapper = mapper;
            _adminWriteRepository = adminWriteRepository;
            _adminBusinessRules = adminBusinessRules;
            _adminReadRepository = adminReadRepository;
        }

        public async Task<UpdatedAdminResponse> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            await _adminBusinessRules.AdminEmailShouldBeUniqueWhenUpdating(request.Gid, request.Email);

            X.Admin? admin = await _adminReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _adminBusinessRules.AdminShouldExistWhenSelected(admin);

            // Þifre deðiþikliði var mý kontrol et
            if (!string.IsNullOrEmpty(request.Password))
            {
                // Yeni þifreyi hashle
                string passwordHash, passwordSalt;
                HashingHelperForApplicationLayer.CreatePasswordHash(
                    request.Password,
                    out passwordHash,
                    out passwordSalt);

                // Kullanýcý nesnesinin þifre alanlarýný güncelle
                admin.Password = passwordHash;
                admin.PasswordSalt = passwordSalt;

                // Password alanýný request'ten temizle (mapper'ýn üzerine yazmamasý için)
                request.Password = null;
            }

            admin = _mapper.Map(request, admin);

            _adminWriteRepository.Update(admin!);
            await _adminWriteRepository.SaveAsync();
            GetByGidAdminResponse obj = _mapper.Map<GetByGidAdminResponse>(admin);

            return new()
            {
                Title = AdminsBusinessMessages.ProcessCompleted,
                Message = AdminsBusinessMessages.SuccessCreatedAdminMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
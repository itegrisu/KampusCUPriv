using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Application.Features.GeneralFeatures.Admins.Rules;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Application.Helpers;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Admins.Commands.Create;

public class CreateAdminCommand : IRequest<CreatedAdminResponse>
{
    public Guid GidClubFK { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, CreatedAdminResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminWriteRepository _adminWriteRepository;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly AdminBusinessRules _adminBusinessRules;

        public CreateAdminCommandHandler(IMapper mapper, IAdminWriteRepository adminWriteRepository,
                                         AdminBusinessRules adminBusinessRules, IAdminReadRepository adminReadRepository)
        {
            _mapper = mapper;
            _adminWriteRepository = adminWriteRepository;
            _adminBusinessRules = adminBusinessRules;
            _adminReadRepository = adminReadRepository;
        }

        public async Task<CreatedAdminResponse> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            await _adminBusinessRules.AdminEmailShouldBeUnique(request.Email);
            //int maxRowNo = await _adminReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Admin admin = _mapper.Map<X.Admin>(request);
            //admin.RowNo = maxRowNo + 1;

            // Þifre Hashleme Ýþlemi
            string passwordHash, passwordSalt;
            HashingHelperForApplicationLayer.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            // Hash ve salt deðerlerini kullanýcý nesnesine kaydet
            admin.Password = passwordHash;  // Düz metin þifre yerine hash deðerini sakla
            admin.PasswordSalt = passwordSalt;

            await _adminWriteRepository.AddAsync(admin);
            await _adminWriteRepository.SaveAsync();

            X.Admin savedAdmin = await _adminReadRepository.GetAsync(predicate: x => x.Gid == admin.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidAdminResponse obj = _mapper.Map<GetByGidAdminResponse>(savedAdmin);
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
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;

public class CreatePersonnelPassportInfoCommand : IRequest<CreatedPersonnelPassportInfoResponse>
{
    public Guid GidPersonelFK { get; set; }
    public string PasaportNo { get; set; }
    public DateTime VerilisTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }


    public class CreatePersonnelPassportInfoCommandHandler : IRequestHandler<CreatePersonnelPassportInfoCommand, CreatedPersonnelPassportInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelPassportInfoWriteRepository _personnelPassportInfoWriteRepository;
        private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
        private readonly PersonnelPassportInfoBusinessRules _personnelPassportInfoBusinessRules;

        public CreatePersonnelPassportInfoCommandHandler(IMapper mapper, IPersonnelPassportInfoWriteRepository personnelPassportInfoWriteRepository,
                                         PersonnelPassportInfoBusinessRules personnelPassportInfoBusinessRules, IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository)
        {
            _mapper = mapper;
            _personnelPassportInfoWriteRepository = personnelPassportInfoWriteRepository;
            _personnelPassportInfoBusinessRules = personnelPassportInfoBusinessRules;
            _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
        }

        public async Task<CreatedPersonnelPassportInfoResponse> Handle(CreatePersonnelPassportInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelPassportInfo personnelPassportInfo = _mapper.Map<X.PersonnelPassportInfo>(request);

            await _personnelPassportInfoWriteRepository.AddAsync(personnelPassportInfo);
            await _personnelPassportInfoWriteRepository.SaveAsync();

            X.PersonnelPassportInfo savedPersonnelPassportInfo = await _personnelPassportInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelPassportInfo.Gid, include: x => x.Include(x => x.UserFK));


            GetByGidPersonnelPassportInfoResponse obj = _mapper.Map<GetByGidPersonnelPassportInfoResponse>(savedPersonnelPassportInfo);
            return new()
            {
                Title = PersonnelPassportInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelPassportInfosBusinessMessages.SuccessCreatedPersonnelPassportInfoMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
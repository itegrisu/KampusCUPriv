using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;

public class CreatePersonnelResidenceInfoCommand : IRequest<CreatedPersonnelResidenceInfoResponse>
{
    public Guid GidPersonelFK { get; set; }

public string OturumSeriNo { get; set; }
public DateTime VerilisTarihi { get; set; }
public DateTime GecerlilikTarihi { get; set; }
public string? Belge { get; set; }
public string? Aciklama { get; set; }



    public class CreatePersonnelResidenceInfoCommandHandler : IRequestHandler<CreatePersonnelResidenceInfoCommand, CreatedPersonnelResidenceInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelResidenceInfoWriteRepository _personnelResidenceInfoWriteRepository;
        private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
        private readonly PersonnelResidenceInfoBusinessRules _personnelResidenceInfoBusinessRules;

        public CreatePersonnelResidenceInfoCommandHandler(IMapper mapper, IPersonnelResidenceInfoWriteRepository personnelResidenceInfoWriteRepository,
                                         PersonnelResidenceInfoBusinessRules personnelResidenceInfoBusinessRules, IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository)
        {
            _mapper = mapper;
            _personnelResidenceInfoWriteRepository = personnelResidenceInfoWriteRepository;
            _personnelResidenceInfoBusinessRules = personnelResidenceInfoBusinessRules;
            _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
        }

        public async Task<CreatedPersonnelResidenceInfoResponse> Handle(CreatePersonnelResidenceInfoCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _personnelResidenceInfoReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.PersonnelResidenceInfo personnelResidenceInfo = _mapper.Map<X.PersonnelResidenceInfo>(request);
            //personnelResidenceInfo.RowNo = maxRowNo + 1;

            await _personnelResidenceInfoWriteRepository.AddAsync(personnelResidenceInfo);
            await _personnelResidenceInfoWriteRepository.SaveAsync();

			X.PersonnelResidenceInfo savedPersonnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelResidenceInfo.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelResidenceInfoResponse obj = _mapper.Map<GetByGidPersonnelResidenceInfoResponse>(savedPersonnelResidenceInfo);
            return new()
            {           
                Title = PersonnelResidenceInfosBusinessMessages.ProcessCompleted,
                Message = PersonnelResidenceInfosBusinessMessages.SuccessCreatedPersonnelResidenceInfoMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
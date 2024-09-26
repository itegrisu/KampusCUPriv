using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Update;

public class UpdatePersonnelResidenceInfoCommand : IRequest<UpdatedPersonnelResidenceInfoResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonelFK { get; set; }

    public string OturumSeriNo { get; set; }
    public DateTime VerilisTarihi { get; set; }
    public DateTime GecerlilikTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }



    public class UpdatePersonnelResidenceInfoCommandHandler : IRequestHandler<UpdatePersonnelResidenceInfoCommand, UpdatedPersonnelResidenceInfoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelResidenceInfoWriteRepository _personnelResidenceInfoWriteRepository;
        private readonly IPersonnelResidenceInfoReadRepository _personnelResidenceInfoReadRepository;
        private readonly PersonnelResidenceInfoBusinessRules _personnelResidenceInfoBusinessRules;

        public UpdatePersonnelResidenceInfoCommandHandler(IMapper mapper, IPersonnelResidenceInfoWriteRepository personnelResidenceInfoWriteRepository,
                                         PersonnelResidenceInfoBusinessRules personnelResidenceInfoBusinessRules, IPersonnelResidenceInfoReadRepository personnelResidenceInfoReadRepository)
        {
            _mapper = mapper;
            _personnelResidenceInfoWriteRepository = personnelResidenceInfoWriteRepository;
            _personnelResidenceInfoBusinessRules = personnelResidenceInfoBusinessRules;
            _personnelResidenceInfoReadRepository = personnelResidenceInfoReadRepository;
        }

        public async Task<UpdatedPersonnelResidenceInfoResponse> Handle(UpdatePersonnelResidenceInfoCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelResidenceInfo? personnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelResidenceInfoBusinessRules.PersonnelResidenceInfoShouldExistWhenSelected(personnelResidenceInfo);
            await _personnelResidenceInfoBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            personnelResidenceInfo = _mapper.Map(request, personnelResidenceInfo);

            _personnelResidenceInfoWriteRepository.Update(personnelResidenceInfo!);
            await _personnelResidenceInfoWriteRepository.SaveAsync();

            X.PersonnelResidenceInfo updatedPersonnelResidenceInfo = await _personnelResidenceInfoReadRepository.GetAsync(predicate: x => x.Gid == personnelResidenceInfo.Gid, include: x => x.Include(x => x.UserFK));


            GetByGidPersonnelResidenceInfoResponse obj = _mapper.Map<GetByGidPersonnelResidenceInfoResponse>(updatedPersonnelResidenceInfo);

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
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Update;

public class UpdatePersonnelGraduatedSchoolCommand : IRequest<UpdatedPersonnelGraduatedSchoolResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonelFK { get; set; }

    public EnumEgitimKurumuTuru EgitimKurumuTuru { get; set; }
    public string OkulBilgisi { get; set; }
    public string BolumBilgisi { get; set; }
    public int BaslamaYili { get; set; }
    public DateTime? MezuniyetTarihi { get; set; }
    public string? Belge { get; set; }
    public string? Aciklama { get; set; }



    public class UpdatePersonnelGraduatedSchoolCommandHandler : IRequestHandler<UpdatePersonnelGraduatedSchoolCommand, UpdatedPersonnelGraduatedSchoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelGraduatedSchoolWriteRepository _personnelGraduatedSchoolWriteRepository;
        private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
        private readonly PersonnelGraduatedSchoolBusinessRules _personnelGraduatedSchoolBusinessRules;

        public UpdatePersonnelGraduatedSchoolCommandHandler(IMapper mapper, IPersonnelGraduatedSchoolWriteRepository personnelGraduatedSchoolWriteRepository,
                                         PersonnelGraduatedSchoolBusinessRules personnelGraduatedSchoolBusinessRules, IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository)
        {
            _mapper = mapper;
            _personnelGraduatedSchoolWriteRepository = personnelGraduatedSchoolWriteRepository;
            _personnelGraduatedSchoolBusinessRules = personnelGraduatedSchoolBusinessRules;
            _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
        }

        public async Task<UpdatedPersonnelGraduatedSchoolResponse> Handle(UpdatePersonnelGraduatedSchoolCommand request, CancellationToken cancellationToken)
        {

            X.PersonnelGraduatedSchool? personnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _personnelGraduatedSchoolBusinessRules.PersonnelGraduatedSchoolShouldExistWhenSelected(personnelGraduatedSchool);
            await _personnelGraduatedSchoolBusinessRules.UserShouldExistWhenSelected(request.GidPersonelFK);
            personnelGraduatedSchool = _mapper.Map(request, personnelGraduatedSchool);

            _personnelGraduatedSchoolWriteRepository.Update(personnelGraduatedSchool!);
            await _personnelGraduatedSchoolWriteRepository.SaveAsync();

            X.PersonnelGraduatedSchool updatedPersonnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetAsync(predicate: x => x.Gid == personnelGraduatedSchool.Gid, include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelGraduatedSchoolResponse obj = _mapper.Map<GetByGidPersonnelGraduatedSchoolResponse>(updatedPersonnelGraduatedSchool);

            return new()
            {
                Title = PersonnelGraduatedSchoolsBusinessMessages.ProcessCompleted,
                Message = PersonnelGraduatedSchoolsBusinessMessages.SuccessCreatedPersonnelGraduatedSchoolMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
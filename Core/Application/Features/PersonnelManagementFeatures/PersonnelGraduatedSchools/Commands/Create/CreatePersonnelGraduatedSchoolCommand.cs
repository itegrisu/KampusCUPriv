using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Create;

public class CreatePersonnelGraduatedSchoolCommand : IRequest<CreatedPersonnelGraduatedSchoolResponse>
{
    public Guid GidPersonnelFK { get; set; }
    public EnumEducationalInstitutionType EducationalInstitutionType { get; set; }
    public string SchoolInfo { get; set; }
    public string DepartmentInfo { get; set; }
    public int StartYear { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class CreatePersonnelGraduatedSchoolCommandHandler : IRequestHandler<CreatePersonnelGraduatedSchoolCommand, CreatedPersonnelGraduatedSchoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelGraduatedSchoolWriteRepository _personnelGraduatedSchoolWriteRepository;
        private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
        private readonly PersonnelGraduatedSchoolBusinessRules _personnelGraduatedSchoolBusinessRules;

        public CreatePersonnelGraduatedSchoolCommandHandler(IMapper mapper, IPersonnelGraduatedSchoolWriteRepository personnelGraduatedSchoolWriteRepository,
                                         PersonnelGraduatedSchoolBusinessRules personnelGraduatedSchoolBusinessRules, IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository)
        {
            _mapper = mapper;
            _personnelGraduatedSchoolWriteRepository = personnelGraduatedSchoolWriteRepository;
            _personnelGraduatedSchoolBusinessRules = personnelGraduatedSchoolBusinessRules;
            _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
        }

        public async Task<CreatedPersonnelGraduatedSchoolResponse> Handle(CreatePersonnelGraduatedSchoolCommand request, CancellationToken cancellationToken)
        {
            await _personnelGraduatedSchoolBusinessRules.UserShouldExistWhenSelected(request.GidPersonnelFK);

            X.PersonnelGraduatedSchool personnelGraduatedSchool = _mapper.Map<X.PersonnelGraduatedSchool>(request);


            await _personnelGraduatedSchoolWriteRepository.AddAsync(personnelGraduatedSchool);
            await _personnelGraduatedSchoolWriteRepository.SaveAsync();

            X.PersonnelGraduatedSchool savedPersonnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetAsync(predicate: x => x.Gid == personnelGraduatedSchool.Gid, include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelGraduatedSchoolResponse obj = _mapper.Map<GetByGidPersonnelGraduatedSchoolResponse>(savedPersonnelGraduatedSchool);
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
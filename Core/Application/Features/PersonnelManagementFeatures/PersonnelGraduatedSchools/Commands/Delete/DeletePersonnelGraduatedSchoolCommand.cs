using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Delete;

public class DeletePersonnelGraduatedSchoolCommand : IRequest<DeletedPersonnelGraduatedSchoolResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelGraduatedSchoolCommandHandler : IRequestHandler<DeletePersonnelGraduatedSchoolCommand, DeletedPersonnelGraduatedSchoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
        private readonly IPersonnelGraduatedSchoolWriteRepository _personnelGraduatedSchoolWriteRepository;
        private readonly PersonnelGraduatedSchoolBusinessRules _personnelGraduatedSchoolBusinessRules;

        public DeletePersonnelGraduatedSchoolCommandHandler(IMapper mapper, IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository,
                                         PersonnelGraduatedSchoolBusinessRules personnelGraduatedSchoolBusinessRules, IPersonnelGraduatedSchoolWriteRepository personnelGraduatedSchoolWriteRepository)
        {
            _mapper = mapper;
            _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
            _personnelGraduatedSchoolBusinessRules = personnelGraduatedSchoolBusinessRules;
            _personnelGraduatedSchoolWriteRepository = personnelGraduatedSchoolWriteRepository;
        }

        public async Task<DeletedPersonnelGraduatedSchoolResponse> Handle(DeletePersonnelGraduatedSchoolCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelGraduatedSchool? personnelGraduatedSchool = await _personnelGraduatedSchoolReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelGraduatedSchoolBusinessRules.PersonnelGraduatedSchoolShouldExistWhenSelected(personnelGraduatedSchool);
            personnelGraduatedSchool.DataState = Core.Enum.DataState.Deleted;

            _personnelGraduatedSchoolWriteRepository.Update(personnelGraduatedSchool);
            await _personnelGraduatedSchoolWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelGraduatedSchoolsBusinessMessages.ProcessCompleted,
                Message = PersonnelGraduatedSchoolsBusinessMessages.SuccessDeletedPersonnelGraduatedSchoolMessage,
                IsValid = true
            };
        }
    }
}
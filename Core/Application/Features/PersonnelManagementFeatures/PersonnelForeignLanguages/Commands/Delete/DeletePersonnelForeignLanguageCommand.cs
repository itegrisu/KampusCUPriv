using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Delete;

public class DeletePersonnelForeignLanguageCommand : IRequest<DeletedPersonnelForeignLanguageResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelForeignLanguageCommandHandler : IRequestHandler<DeletePersonnelForeignLanguageCommand, DeletedPersonnelForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
        private readonly IPersonnelForeignLanguageWriteRepository _personnelForeignLanguageWriteRepository;
        private readonly PersonnelForeignLanguageBusinessRules _personnelForeignLanguageBusinessRules;

        public DeletePersonnelForeignLanguageCommandHandler(IMapper mapper, IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository,
                                         PersonnelForeignLanguageBusinessRules personnelForeignLanguageBusinessRules, IPersonnelForeignLanguageWriteRepository personnelForeignLanguageWriteRepository)
        {
            _mapper = mapper;
            _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
            _personnelForeignLanguageBusinessRules = personnelForeignLanguageBusinessRules;
            _personnelForeignLanguageWriteRepository = personnelForeignLanguageWriteRepository;
        }

        public async Task<DeletedPersonnelForeignLanguageResponse> Handle(DeletePersonnelForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelForeignLanguage? personnelForeignLanguage = await _personnelForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelForeignLanguageBusinessRules.PersonnelForeignLanguageShouldExistWhenSelected(personnelForeignLanguage);
            personnelForeignLanguage.DataState = Core.Enum.DataState.Deleted;

            _personnelForeignLanguageWriteRepository.Update(personnelForeignLanguage);
            await _personnelForeignLanguageWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = PersonnelForeignLanguagesBusinessMessages.SuccessDeletedPersonnelForeignLanguageMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Constants;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Rules;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Delete;

public class DeleteForeignLanguageCommand : IRequest<DeletedForeignLanguageResponse>
{
	public Guid Gid { get; set; }

    public class DeleteForeignLanguageCommandHandler : IRequestHandler<DeleteForeignLanguageCommand, DeletedForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;
        private readonly IForeignLanguageWriteRepository _foreignLanguageWriteRepository;
        private readonly ForeignLanguageBusinessRules _foreignLanguageBusinessRules;

        public DeleteForeignLanguageCommandHandler(IMapper mapper, IForeignLanguageReadRepository foreignLanguageReadRepository,
                                         ForeignLanguageBusinessRules foreignLanguageBusinessRules, IForeignLanguageWriteRepository foreignLanguageWriteRepository)
        {
            _mapper = mapper;
            _foreignLanguageReadRepository = foreignLanguageReadRepository;
            _foreignLanguageBusinessRules = foreignLanguageBusinessRules;
            _foreignLanguageWriteRepository = foreignLanguageWriteRepository;
        }

        public async Task<DeletedForeignLanguageResponse> Handle(DeleteForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.ForeignLanguage? foreignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _foreignLanguageBusinessRules.ForeignLanguageShouldExistWhenSelected(foreignLanguage);
            foreignLanguage.DataState = Core.Enum.DataState.Deleted;

            _foreignLanguageWriteRepository.Update(foreignLanguage);
            await _foreignLanguageWriteRepository.SaveAsync();

            return new()
            {
                Title = ForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = ForeignLanguagesBusinessMessages.SuccessDeletedForeignLanguageMessage,
                IsValid = true
            };
        }
    }
}
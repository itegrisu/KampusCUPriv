using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Delete;

public class DeletePartTimeWorkerForeignLanguageCommand : IRequest<DeletedPartTimeWorkerForeignLanguageResponse>
{
	public Guid Gid { get; set; }

    public class DeletePartTimeWorkerForeignLanguageCommandHandler : IRequestHandler<DeletePartTimeWorkerForeignLanguageCommand, DeletedPartTimeWorkerForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
        private readonly IPartTimeWorkerForeignLanguageWriteRepository _partTimeWorkerForeignLanguageWriteRepository;
        private readonly PartTimeWorkerForeignLanguageBusinessRules _partTimeWorkerForeignLanguageBusinessRules;

        public DeletePartTimeWorkerForeignLanguageCommandHandler(IMapper mapper, IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository,
                                         PartTimeWorkerForeignLanguageBusinessRules partTimeWorkerForeignLanguageBusinessRules, IPartTimeWorkerForeignLanguageWriteRepository partTimeWorkerForeignLanguageWriteRepository)
        {
            _mapper = mapper;
            _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
            _partTimeWorkerForeignLanguageBusinessRules = partTimeWorkerForeignLanguageBusinessRules;
            _partTimeWorkerForeignLanguageWriteRepository = partTimeWorkerForeignLanguageWriteRepository;
        }

        public async Task<DeletedPartTimeWorkerForeignLanguageResponse> Handle(DeletePartTimeWorkerForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorkerForeignLanguage? partTimeWorkerForeignLanguage = await _partTimeWorkerForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _partTimeWorkerForeignLanguageBusinessRules.PartTimeWorkerForeignLanguageShouldExistWhenSelected(partTimeWorkerForeignLanguage);
            partTimeWorkerForeignLanguage.DataState = Core.Enum.DataState.Deleted;

            _partTimeWorkerForeignLanguageWriteRepository.Update(partTimeWorkerForeignLanguage);
            await _partTimeWorkerForeignLanguageWriteRepository.SaveAsync();

            return new()
            {
                Title = PartTimeWorkerForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkerForeignLanguagesBusinessMessages.SuccessDeletedPartTimeWorkerForeignLanguageMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Update;

public class UpdatePartTimeWorkerForeignLanguageCommand : IRequest<UpdatedPartTimeWorkerForeignLanguageResponse>
{
    public Guid Gid { get; set; }

	public Guid GidPartTimeWorkerFK { get; set; }
public Guid GidForeignLanguageFK { get; set; }




    public class UpdatePartTimeWorkerForeignLanguageCommandHandler : IRequestHandler<UpdatePartTimeWorkerForeignLanguageCommand, UpdatedPartTimeWorkerForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerForeignLanguageWriteRepository _partTimeWorkerForeignLanguageWriteRepository;
        private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
        private readonly PartTimeWorkerForeignLanguageBusinessRules _partTimeWorkerForeignLanguageBusinessRules;

        public UpdatePartTimeWorkerForeignLanguageCommandHandler(IMapper mapper, IPartTimeWorkerForeignLanguageWriteRepository partTimeWorkerForeignLanguageWriteRepository,
                                         PartTimeWorkerForeignLanguageBusinessRules partTimeWorkerForeignLanguageBusinessRules, IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerForeignLanguageWriteRepository = partTimeWorkerForeignLanguageWriteRepository;
            _partTimeWorkerForeignLanguageBusinessRules = partTimeWorkerForeignLanguageBusinessRules;
            _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
        }

        public async Task<UpdatedPartTimeWorkerForeignLanguageResponse> Handle(UpdatePartTimeWorkerForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.PartTimeWorkerForeignLanguage? partTimeWorkerForeignLanguage = await _partTimeWorkerForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _partTimeWorkerForeignLanguageBusinessRules.PartTimeWorkerForeignLanguageShouldExistWhenSelected(partTimeWorkerForeignLanguage);
            partTimeWorkerForeignLanguage = _mapper.Map(request, partTimeWorkerForeignLanguage);

            _partTimeWorkerForeignLanguageWriteRepository.Update(partTimeWorkerForeignLanguage!);
            await _partTimeWorkerForeignLanguageWriteRepository.SaveAsync();
            GetByGidPartTimeWorkerForeignLanguageResponse obj = _mapper.Map<GetByGidPartTimeWorkerForeignLanguageResponse>(partTimeWorkerForeignLanguage);

            return new()
            {
                Title = PartTimeWorkerForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = PartTimeWorkerForeignLanguagesBusinessMessages.SuccessCreatedPartTimeWorkerForeignLanguageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
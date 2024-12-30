using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Constants;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Create;

public class CreatePartTimeWorkerForeignLanguageCommand : IRequest<CreatedPartTimeWorkerForeignLanguageResponse>
{
    public Guid GidPartTimeWorkerFK { get; set; }
    public Guid GidForeignLanguageFK { get; set; }




    public class CreatePartTimeWorkerForeignLanguageCommandHandler : IRequestHandler<CreatePartTimeWorkerForeignLanguageCommand, CreatedPartTimeWorkerForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPartTimeWorkerForeignLanguageWriteRepository _partTimeWorkerForeignLanguageWriteRepository;
        private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
        private readonly PartTimeWorkerForeignLanguageBusinessRules _partTimeWorkerForeignLanguageBusinessRules;

        public CreatePartTimeWorkerForeignLanguageCommandHandler(IMapper mapper, IPartTimeWorkerForeignLanguageWriteRepository partTimeWorkerForeignLanguageWriteRepository,
                                         PartTimeWorkerForeignLanguageBusinessRules partTimeWorkerForeignLanguageBusinessRules, IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository)
        {
            _mapper = mapper;
            _partTimeWorkerForeignLanguageWriteRepository = partTimeWorkerForeignLanguageWriteRepository;
            _partTimeWorkerForeignLanguageBusinessRules = partTimeWorkerForeignLanguageBusinessRules;
            _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
        }

        public async Task<CreatedPartTimeWorkerForeignLanguageResponse> Handle(CreatePartTimeWorkerForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            await _partTimeWorkerForeignLanguageBusinessRules.PartTimeWorkerAlreadyExist(request.GidPartTimeWorkerFK);
            await _partTimeWorkerForeignLanguageBusinessRules.ForeignLanguageAlreadyExist(request.GidForeignLanguageFK);
            await _partTimeWorkerForeignLanguageBusinessRules.IsForeignLanguageAlreadyAdded(request.GidPartTimeWorkerFK, request.GidForeignLanguageFK);

            //int maxRowNo = await _partTimeWorkerForeignLanguageReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.PartTimeWorkerForeignLanguage partTimeWorkerForeignLanguage = _mapper.Map<X.PartTimeWorkerForeignLanguage>(request);
            //partTimeWorkerForeignLanguage.RowNo = maxRowNo + 1;

            await _partTimeWorkerForeignLanguageWriteRepository.AddAsync(partTimeWorkerForeignLanguage);
            await _partTimeWorkerForeignLanguageWriteRepository.SaveAsync();

            X.PartTimeWorkerForeignLanguage savedPartTimeWorkerForeignLanguage = await _partTimeWorkerForeignLanguageReadRepository.GetAsync(predicate: x => x.Gid == partTimeWorkerForeignLanguage.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidPartTimeWorkerForeignLanguageResponse obj = _mapper.Map<GetByGidPartTimeWorkerForeignLanguageResponse>(savedPartTimeWorkerForeignLanguage);
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
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Constants;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Rules;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;

public class CreateForeignLanguageCommand : IRequest<CreatedForeignLanguageResponse>
{

    public string DilAdi { get; set; }
    public string? DilKodu { get; set; }

    public class CreateForeignLanguageCommandHandler : IRequestHandler<CreateForeignLanguageCommand, CreatedForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IForeignLanguageWriteRepository _foreignLanguageWriteRepository;
        private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;
        private readonly ForeignLanguageBusinessRules _foreignLanguageBusinessRules;

        public CreateForeignLanguageCommandHandler(IMapper mapper, IForeignLanguageWriteRepository foreignLanguageWriteRepository,
                                         ForeignLanguageBusinessRules foreignLanguageBusinessRules, IForeignLanguageReadRepository foreignLanguageReadRepository)
        {
            _mapper = mapper;
            _foreignLanguageWriteRepository = foreignLanguageWriteRepository;
            _foreignLanguageBusinessRules = foreignLanguageBusinessRules;
            _foreignLanguageReadRepository = foreignLanguageReadRepository;
        }

        public async Task<CreatedForeignLanguageResponse> Handle(CreateForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            await _foreignLanguageBusinessRules.CheckForeignLanguageNameIsUnique(request.DilAdi);
            await _foreignLanguageBusinessRules.CheckForeignLanguageCodeIsUnique(request.DilKodu);
            X.ForeignLanguage foreignLanguage = _mapper.Map<X.ForeignLanguage>(request);


            await _foreignLanguageWriteRepository.AddAsync(foreignLanguage);
            await _foreignLanguageWriteRepository.SaveAsync();

            X.ForeignLanguage savedForeignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: x => x.Gid == foreignLanguage.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidForeignLanguageResponse obj = _mapper.Map<GetByGidForeignLanguageResponse>(savedForeignLanguage);
            return new()
            {
                Title = ForeignLanguagesBusinessMessages.ProcessCompleted,
                Message = ForeignLanguagesBusinessMessages.SuccessCreatedForeignLanguageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
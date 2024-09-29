using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Constants;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Rules;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Update;

public class UpdateForeignLanguageCommand : IRequest<UpdatedForeignLanguageResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
    public string? LanguageCode { get; set; }


    public class UpdateForeignLanguageCommandHandler : IRequestHandler<UpdateForeignLanguageCommand, UpdatedForeignLanguageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IForeignLanguageWriteRepository _foreignLanguageWriteRepository;
        private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;
        private readonly ForeignLanguageBusinessRules _foreignLanguageBusinessRules;

        public UpdateForeignLanguageCommandHandler(IMapper mapper, IForeignLanguageWriteRepository foreignLanguageWriteRepository,
                                         ForeignLanguageBusinessRules foreignLanguageBusinessRules, IForeignLanguageReadRepository foreignLanguageReadRepository)
        {
            _mapper = mapper;
            _foreignLanguageWriteRepository = foreignLanguageWriteRepository;
            _foreignLanguageBusinessRules = foreignLanguageBusinessRules;
            _foreignLanguageReadRepository = foreignLanguageReadRepository;
        }

        public async Task<UpdatedForeignLanguageResponse> Handle(UpdateForeignLanguageCommand request, CancellationToken cancellationToken)
        {
            X.ForeignLanguage? foreignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _foreignLanguageBusinessRules.ForeignLanguageShouldExistWhenSelected(foreignLanguage);
            await _foreignLanguageBusinessRules.CheckForeignLanguageNameIsUnique(request.Name, request.Gid);
            await _foreignLanguageBusinessRules.CheckForeignLanguageCodeIsUnique(request.LanguageCode, request.Gid);
            foreignLanguage = _mapper.Map(request, foreignLanguage);

            _foreignLanguageWriteRepository.Update(foreignLanguage!);
            await _foreignLanguageWriteRepository.SaveAsync();
            GetByGidForeignLanguageResponse obj = _mapper.Map<GetByGidForeignLanguageResponse>(foreignLanguage);

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
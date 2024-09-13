using Application.Features.DefinitionManagementFeatures.ForeignLanguages.Rules;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetByGid
{
    public class GetByGidForeignLanguageQuery : IRequest<GetByGidForeignLanguageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidForeignLanguageQueryHandler : IRequestHandler<GetByGidForeignLanguageQuery, GetByGidForeignLanguageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;
            private readonly ForeignLanguageBusinessRules _foreignLanguageBusinessRules;

            public GetByGidForeignLanguageQueryHandler(IMapper mapper, IForeignLanguageReadRepository foreignLanguageReadRepository, ForeignLanguageBusinessRules foreignLanguageBusinessRules)
            {
                _mapper = mapper;
                _foreignLanguageReadRepository = foreignLanguageReadRepository;
                _foreignLanguageBusinessRules = foreignLanguageBusinessRules;
            }

            public async Task<GetByGidForeignLanguageResponse> Handle(GetByGidForeignLanguageQuery request, CancellationToken cancellationToken)
            {
                X.ForeignLanguage? foreignLanguage = await _foreignLanguageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _foreignLanguageBusinessRules.ForeignLanguageShouldExistWhenSelected(foreignLanguage);

                GetByGidForeignLanguageResponse response = _mapper.Map<GetByGidForeignLanguageResponse>(foreignLanguage);
                return response;
            }
        }
    }
}
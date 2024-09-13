using Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid
{
    public class GetByGidPersonnelForeignLanguageQuery : IRequest<GetByGidPersonnelForeignLanguageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelForeignLanguageQueryHandler : IRequestHandler<GetByGidPersonnelForeignLanguageQuery, GetByGidPersonnelForeignLanguageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelForeignLanguageReadRepository _personnelForeignLanguageReadRepository;
            private readonly PersonnelForeignLanguageBusinessRules _personnelForeignLanguageBusinessRules;

            public GetByGidPersonnelForeignLanguageQueryHandler(IMapper mapper, IPersonnelForeignLanguageReadRepository personnelForeignLanguageReadRepository, PersonnelForeignLanguageBusinessRules personnelForeignLanguageBusinessRules)
            {
                _mapper = mapper;
                _personnelForeignLanguageReadRepository = personnelForeignLanguageReadRepository;
                _personnelForeignLanguageBusinessRules = personnelForeignLanguageBusinessRules;
            }

            public async Task<GetByGidPersonnelForeignLanguageResponse> Handle(GetByGidPersonnelForeignLanguageQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelForeignLanguage? personnelForeignLanguage = await _personnelForeignLanguageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.ForeignLanguageFK));

                await _personnelForeignLanguageBusinessRules.PersonnelForeignLanguageShouldExistWhenSelected(personnelForeignLanguage);

                GetByGidPersonnelForeignLanguageResponse response = _mapper.Map<GetByGidPersonnelForeignLanguageResponse>(personnelForeignLanguage);
                return response;
            }
        }
    }
}
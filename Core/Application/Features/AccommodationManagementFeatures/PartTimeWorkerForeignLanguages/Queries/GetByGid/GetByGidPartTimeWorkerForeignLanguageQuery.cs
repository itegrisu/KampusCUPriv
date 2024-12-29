using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Rules;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerForeignLanguageQuery : IRequest<GetByGidPartTimeWorkerForeignLanguageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPartTimeWorkerForeignLanguageQueryHandler : IRequestHandler<GetByGidPartTimeWorkerForeignLanguageQuery, GetByGidPartTimeWorkerForeignLanguageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPartTimeWorkerForeignLanguageReadRepository _partTimeWorkerForeignLanguageReadRepository;
            private readonly PartTimeWorkerForeignLanguageBusinessRules _partTimeWorkerForeignLanguageBusinessRules;

            public GetByGidPartTimeWorkerForeignLanguageQueryHandler(IMapper mapper, IPartTimeWorkerForeignLanguageReadRepository partTimeWorkerForeignLanguageReadRepository, PartTimeWorkerForeignLanguageBusinessRules partTimeWorkerForeignLanguageBusinessRules)
            {
                _mapper = mapper;
                _partTimeWorkerForeignLanguageReadRepository = partTimeWorkerForeignLanguageReadRepository;
                _partTimeWorkerForeignLanguageBusinessRules = partTimeWorkerForeignLanguageBusinessRules;
            }

            public async Task<GetByGidPartTimeWorkerForeignLanguageResponse> Handle(GetByGidPartTimeWorkerForeignLanguageQuery request, CancellationToken cancellationToken)
            {
                X.PartTimeWorkerForeignLanguage? partTimeWorkerForeignLanguage = await _partTimeWorkerForeignLanguageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _partTimeWorkerForeignLanguageBusinessRules.PartTimeWorkerForeignLanguageShouldExistWhenSelected(partTimeWorkerForeignLanguage);

                GetByGidPartTimeWorkerForeignLanguageResponse response = _mapper.Map<GetByGidPartTimeWorkerForeignLanguageResponse>(partTimeWorkerForeignLanguage);
                return response;
            }
        }
    }
}
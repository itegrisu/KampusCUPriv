using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Rules;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid
{
    public class GetByGidTyreTypeQuery : IRequest<GetByGidTyreTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTyreTypeQueryHandler : IRequestHandler<GetByGidTyreTypeQuery, GetByGidTyreTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITyreTypeReadRepository _tyreTypeReadRepository;
            private readonly TyreTypeBusinessRules _tyreTypeBusinessRules;

            public GetByGidTyreTypeQueryHandler(IMapper mapper, ITyreTypeReadRepository tyreTypeReadRepository, TyreTypeBusinessRules tyreTypeBusinessRules)
            {
                _mapper = mapper;
                _tyreTypeReadRepository = tyreTypeReadRepository;
                _tyreTypeBusinessRules = tyreTypeBusinessRules;
            }

            public async Task<GetByGidTyreTypeResponse> Handle(GetByGidTyreTypeQuery request, CancellationToken cancellationToken)
            {
                X.TyreType? tyreType = await _tyreTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _tyreTypeBusinessRules.TyreTypeShouldExistWhenSelected(tyreType);

                GetByGidTyreTypeResponse response = _mapper.Map<GetByGidTyreTypeResponse>(tyreType);
                return response;
            }
        }
    }
}
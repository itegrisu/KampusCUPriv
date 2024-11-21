using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using Application.Features.DefinitionManagementFeatures.Districts.Rules;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid
{
    public class GetByGidDistrictQuery : IRequest<GetByGidDistrictResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidDistrictQueryHandler : IRequestHandler<GetByGidDistrictQuery, GetByGidDistrictResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDistrictReadRepository _districtReadRepository;
            private readonly DistrictBusinessRules _districtBusinessRules;

            public GetByGidDistrictQueryHandler(IMapper mapper, IDistrictReadRepository districtReadRepository, DistrictBusinessRules districtBusinessRules)
            {
                _mapper = mapper;
                _districtReadRepository = districtReadRepository;
                _districtBusinessRules = districtBusinessRules;
            }

            public async Task<GetByGidDistrictResponse> Handle(GetByGidDistrictQuery request, CancellationToken cancellationToken)
            {
                X.District? district = await _districtReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                      include: x => x.Include(x => x.CityFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _districtBusinessRules.DistrictShouldExistWhenSelected(district);

                GetByGidDistrictResponse response = _mapper.Map<GetByGidDistrictResponse>(district);
                return response;
            }
        }
    }
}
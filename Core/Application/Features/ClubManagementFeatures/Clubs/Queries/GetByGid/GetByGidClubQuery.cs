using AutoMapper;
using MediatR;
using X = Domain.Entities.ClubManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.ClubFeatures.Clubs.Rules;
using Application.Repositories.ClubManagementRepos.ClubRepo;

namespace Application.Features.ClubFeatures.Clubs.Queries.GetByGid
{
    public class GetByGidClubQuery : IRequest<GetByGidClubResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidClubQueryHandler : IRequestHandler<GetByGidClubQuery, GetByGidClubResponse>
        {
            private readonly IMapper _mapper;
            private readonly IClubReadRepository _clubReadRepository;
            private readonly ClubBusinessRules _clubBusinessRules;

            public GetByGidClubQueryHandler(IMapper mapper, IClubReadRepository clubReadRepository, ClubBusinessRules clubBusinessRules)
            {
                _mapper = mapper;
                _clubReadRepository = clubReadRepository;
                _clubBusinessRules = clubBusinessRules;
            }

            public async Task<GetByGidClubResponse> Handle(GetByGidClubQuery request, CancellationToken cancellationToken)
            {
                X.Club? club = await _clubReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _clubBusinessRules.ClubShouldExistWhenSelected(club);

                GetByGidClubResponse response = _mapper.Map<GetByGidClubResponse>(club);
                return response;
            }
        }
    }
}
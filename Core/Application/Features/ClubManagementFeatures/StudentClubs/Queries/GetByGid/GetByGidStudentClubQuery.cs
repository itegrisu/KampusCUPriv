using AutoMapper;
using MediatR;
using X = Domain.Entities.ClubManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.ClubFeatures.StudentClubs.Rules;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;

namespace Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid
{
    public class GetByGidStudentClubQuery : IRequest<GetByGidStudentClubResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStudentClubQueryHandler : IRequestHandler<GetByGidStudentClubQuery, GetByGidStudentClubResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStudentClubReadRepository _studentClubReadRepository;
            private readonly StudentClubBusinessRules _studentClubBusinessRules;

            public GetByGidStudentClubQueryHandler(IMapper mapper, IStudentClubReadRepository studentClubReadRepository, StudentClubBusinessRules studentClubBusinessRules)
            {
                _mapper = mapper;
                _studentClubReadRepository = studentClubReadRepository;
                _studentClubBusinessRules = studentClubBusinessRules;
            }

            public async Task<GetByGidStudentClubResponse> Handle(GetByGidStudentClubQuery request, CancellationToken cancellationToken)
            {
                X.StudentClub? studentClub = await _studentClubReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _studentClubBusinessRules.StudentClubShouldExistWhenSelected(studentClub);

                GetByGidStudentClubResponse response = _mapper.Map<GetByGidStudentClubResponse>(studentClub);
                return response;
            }
        }
    }
}
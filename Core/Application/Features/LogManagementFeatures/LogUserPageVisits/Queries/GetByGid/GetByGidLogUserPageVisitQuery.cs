using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Rules;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid
{
    public class GetByGidLogUserPageVisitQuery : IRequest<GetByGidLogUserPageVisitResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogUserPageVisitQueryHandler : IRequestHandler<GetByGidLogUserPageVisitQuery, GetByGidLogUserPageVisitResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
            private readonly LogUserPageVisitBusinessRules _logUserPageVisitBusinessRules;

            public GetByGidLogUserPageVisitQueryHandler(IMapper mapper, ILogUserPageVisitReadRepository logUserPageVisitReadRepository, LogUserPageVisitBusinessRules logUserPageVisitBusinessRules)
            {
                _mapper = mapper;
                _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
                _logUserPageVisitBusinessRules = logUserPageVisitBusinessRules;
            }

            public async Task<GetByGidLogUserPageVisitResponse> Handle(GetByGidLogUserPageVisitQuery request, CancellationToken cancellationToken)
            {
                X.LogUserPageVisit? logUserPageVisit = await _logUserPageVisitReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logUserPageVisitBusinessRules.LogUserPageVisitShouldExistWhenSelected(logUserPageVisit);

                GetByGidLogUserPageVisitResponse response = _mapper.Map<GetByGidLogUserPageVisitResponse>(logUserPageVisit);
                return response;
            }
        }
    }
}
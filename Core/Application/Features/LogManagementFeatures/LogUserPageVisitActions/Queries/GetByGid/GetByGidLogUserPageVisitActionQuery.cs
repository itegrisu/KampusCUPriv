using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Rules;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid
{
    public class GetByGidLogUserPageVisitActionQuery : IRequest<GetByGidLogUserPageVisitActionResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogUserPageVisitActionQueryHandler : IRequestHandler<GetByGidLogUserPageVisitActionQuery, GetByGidLogUserPageVisitActionResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogUserPageVisitActionReadRepository _logUserPageVisitActionReadRepository;
            private readonly LogUserPageVisitActionBusinessRules _logUserPageVisitActionBusinessRules;

            public GetByGidLogUserPageVisitActionQueryHandler(IMapper mapper, ILogUserPageVisitActionReadRepository logUserPageVisitActionReadRepository, LogUserPageVisitActionBusinessRules logUserPageVisitActionBusinessRules)
            {
                _mapper = mapper;
                _logUserPageVisitActionReadRepository = logUserPageVisitActionReadRepository;
                _logUserPageVisitActionBusinessRules = logUserPageVisitActionBusinessRules;
            }

            public async Task<GetByGidLogUserPageVisitActionResponse> Handle(GetByGidLogUserPageVisitActionQuery request, CancellationToken cancellationToken)
            {
                X.LogUserPageVisitAction? logUserPageVisitAction = await _logUserPageVisitActionReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logUserPageVisitActionBusinessRules.LogUserPageVisitActionShouldExistWhenSelected(logUserPageVisitAction);

                GetByGidLogUserPageVisitActionResponse response = _mapper.Map<GetByGidLogUserPageVisitActionResponse>(logUserPageVisitAction);
                return response;
            }
        }
    }
}
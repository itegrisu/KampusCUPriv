using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Rules;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid
{
    public class GetByGidLogAuthorizationErrorQuery : IRequest<GetByGidLogAuthorizationErrorResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogAuthorizationErrorQueryHandler : IRequestHandler<GetByGidLogAuthorizationErrorQuery, GetByGidLogAuthorizationErrorResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogAuthorizationErrorReadRepository _logAuthorizationErrorReadRepository;
            private readonly LogAuthorizationErrorBusinessRules _logAuthorizationErrorBusinessRules;

            public GetByGidLogAuthorizationErrorQueryHandler(IMapper mapper, ILogAuthorizationErrorReadRepository logAuthorizationErrorReadRepository, LogAuthorizationErrorBusinessRules logAuthorizationErrorBusinessRules)
            {
                _mapper = mapper;
                _logAuthorizationErrorReadRepository = logAuthorizationErrorReadRepository;
                _logAuthorizationErrorBusinessRules = logAuthorizationErrorBusinessRules;
            }

            public async Task<GetByGidLogAuthorizationErrorResponse> Handle(GetByGidLogAuthorizationErrorQuery request, CancellationToken cancellationToken)
            {
                X.LogAuthorizationError? logAuthorizationError = await _logAuthorizationErrorReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logAuthorizationErrorBusinessRules.LogAuthorizationErrorShouldExistWhenSelected(logAuthorizationError);

                GetByGidLogAuthorizationErrorResponse response = _mapper.Map<GetByGidLogAuthorizationErrorResponse>(logAuthorizationError);
                return response;
            }
        }
    }
}
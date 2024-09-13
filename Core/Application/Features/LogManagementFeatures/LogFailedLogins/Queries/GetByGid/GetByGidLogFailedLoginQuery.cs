using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Application.Features.LogManagementFeatures.LogFailedLogins.Rules;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid
{
    public class GetByGidLogFailedLoginQuery : IRequest<GetByGidLogFailedLoginResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogFailedLoginQueryHandler : IRequestHandler<GetByGidLogFailedLoginQuery, GetByGidLogFailedLoginResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
            private readonly LogFailedLoginBusinessRules _logFailedLoginBusinessRules;

            public GetByGidLogFailedLoginQueryHandler(IMapper mapper, ILogFailedLoginReadRepository logFailedLoginReadRepository, LogFailedLoginBusinessRules logFailedLoginBusinessRules)
            {
                _mapper = mapper;
                _logFailedLoginReadRepository = logFailedLoginReadRepository;
                _logFailedLoginBusinessRules = logFailedLoginBusinessRules;
            }

            public async Task<GetByGidLogFailedLoginResponse> Handle(GetByGidLogFailedLoginQuery request, CancellationToken cancellationToken)
            {
                X.LogFailedLogin? logFailedLogin = await _logFailedLoginReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logFailedLoginBusinessRules.LogFailedLoginShouldExistWhenSelected(logFailedLogin);

                GetByGidLogFailedLoginResponse response = _mapper.Map<GetByGidLogFailedLoginResponse>(logFailedLogin);
                return response;
            }
        }
    }
}
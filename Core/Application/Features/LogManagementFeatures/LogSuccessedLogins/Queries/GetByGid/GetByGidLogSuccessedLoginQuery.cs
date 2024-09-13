using AutoMapper;
using MediatR;
using X = Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid
{
    public class GetByGidLogSuccessedLoginQuery : IRequest<GetByGidLogSuccessedLoginResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidLogSuccessedLoginQueryHandler : IRequestHandler<GetByGidLogSuccessedLoginQuery, GetByGidLogSuccessedLoginResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
            private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

            public GetByGidLogSuccessedLoginQueryHandler(IMapper mapper, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules)
            {
                _mapper = mapper;
                _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
                _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
            }

            public async Task<GetByGidLogSuccessedLoginResponse> Handle(GetByGidLogSuccessedLoginQuery request, CancellationToken cancellationToken)
            {
                X.LogSuccessedLogin? logSuccessedLogin = await _logSuccessedLoginReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _logSuccessedLoginBusinessRules.LogSuccessedLoginShouldExistWhenSelected(logSuccessedLogin);

                GetByGidLogSuccessedLoginResponse response = _mapper.Map<GetByGidLogSuccessedLoginResponse>(logSuccessedLogin);
                return response;
            }
        }
    }
}
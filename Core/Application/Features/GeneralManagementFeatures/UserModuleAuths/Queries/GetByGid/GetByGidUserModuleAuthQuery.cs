using Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid
{
    public class GetByGidUserModuleAuthQuery : IRequest<GetByGidUserModuleAuthResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidUserModuleAuthQueryHandler : IRequestHandler<GetByGidUserModuleAuthQuery, GetByGidUserModuleAuthResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
            private readonly UserModuleAuthBusinessRules _userModuleAuthBusinessRules;

            public GetByGidUserModuleAuthQueryHandler(IMapper mapper, IUserModuleAuthReadRepository userModuleAuthReadRepository, UserModuleAuthBusinessRules userModuleAuthBusinessRules)
            {
                _mapper = mapper;
                _userModuleAuthReadRepository = userModuleAuthReadRepository;
                _userModuleAuthBusinessRules = userModuleAuthBusinessRules;
            }

            public async Task<GetByGidUserModuleAuthResponse> Handle(GetByGidUserModuleAuthQuery request, CancellationToken cancellationToken)
            {
                X.UserModuleAuth? userModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));

                await _userModuleAuthBusinessRules.UserModuleAuthShouldExistWhenSelected(userModuleAuth);

                GetByGidUserModuleAuthResponse response = _mapper.Map<GetByGidUserModuleAuthResponse>(userModuleAuth);
                return response;
            }
        }
    }
}
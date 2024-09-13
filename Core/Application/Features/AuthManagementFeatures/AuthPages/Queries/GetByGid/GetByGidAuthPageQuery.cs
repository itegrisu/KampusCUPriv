using Application.Features.AuthManagementFeatures.AuthPages.Rules;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using AutoMapper;
using Domain.Entities.AuthManagements;
using MediatR;

namespace Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;

public class GetByGidAuthPageQuery : IRequest<GetByGidAuthPageResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAuthPageQueryHandler : IRequestHandler<GetByGidAuthPageQuery, GetByGidAuthPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly AuthPageBusinessRules _authPageBusinessRules;

        public GetByIdAuthPageQueryHandler(IMapper mapper, IAuthPageReadRepository authPageReadRepository, AuthPageBusinessRules authPageBusinessRules)
        {
            _mapper = mapper;
            _authPageReadRepository = authPageReadRepository;
            _authPageBusinessRules = authPageBusinessRules;
        }

        public async Task<GetByGidAuthPageResponse> Handle(GetByGidAuthPageQuery request, CancellationToken cancellationToken)
        {
            await _authPageBusinessRules.AuthPageShouldExistWhenSelected(request.Gid);
            AuthPage? authPage = await _authPageReadRepository.GetAsync(predicate: ap => ap.Gid == request.Gid, cancellationToken: cancellationToken);
            

            GetByGidAuthPageResponse response = _mapper.Map<GetByGidAuthPageResponse>(authPage);
            return response;
        }
    }
}
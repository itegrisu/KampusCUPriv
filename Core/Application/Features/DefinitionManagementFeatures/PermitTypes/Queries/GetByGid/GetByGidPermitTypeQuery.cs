using Application.Features.DefinitionManagementFeatures.PermitTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid
{
    public class GetByGidPermitTypeQuery : IRequest<GetByGidPermitTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPermitTypeQueryHandler : IRequestHandler<GetByGidPermitTypeQuery, GetByGidPermitTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPermitTypeReadRepository _permitTypeReadRepository;
            private readonly PermitTypeBusinessRules _permitTypeBusinessRules;

            public GetByGidPermitTypeQueryHandler(IMapper mapper, IPermitTypeReadRepository permitTypeReadRepository, PermitTypeBusinessRules permitTypeBusinessRules)
            {
                _mapper = mapper;
                _permitTypeReadRepository = permitTypeReadRepository;
                _permitTypeBusinessRules = permitTypeBusinessRules;
            }

            public async Task<GetByGidPermitTypeResponse> Handle(GetByGidPermitTypeQuery request, CancellationToken cancellationToken)
            {
                X.PermitType? permitType = await _permitTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _permitTypeBusinessRules.PermitTypeShouldExistWhenSelected(permitType);

                GetByGidPermitTypeResponse response = _mapper.Map<GetByGidPermitTypeResponse>(permitType);
                return response;
            }
        }
    }
}
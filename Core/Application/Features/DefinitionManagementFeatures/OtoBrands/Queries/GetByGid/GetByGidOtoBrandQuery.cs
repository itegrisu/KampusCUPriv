using Application.Features.DefinitionManagementFeatures.OtoBrands.Rules;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid
{
    public class GetByGidOtoBrandQuery : IRequest<GetByGidOtoBrandResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOtoBrandQueryHandler : IRequestHandler<GetByGidOtoBrandQuery, GetByGidOtoBrandResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOtoBrandReadRepository _otoBrandReadRepository;
            private readonly OtoBrandBusinessRules _otoBrandBusinessRules;

            public GetByGidOtoBrandQueryHandler(IMapper mapper, IOtoBrandReadRepository otoBrandReadRepository, OtoBrandBusinessRules otoBrandBusinessRules)
            {
                _mapper = mapper;
                _otoBrandReadRepository = otoBrandReadRepository;
                _otoBrandBusinessRules = otoBrandBusinessRules;
            }

            public async Task<GetByGidOtoBrandResponse> Handle(GetByGidOtoBrandQuery request, CancellationToken cancellationToken)
            {
                X.OtoBrand? otoBrand = await _otoBrandReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _otoBrandBusinessRules.OtoBrandShouldExistWhenSelected(otoBrand);

                GetByGidOtoBrandResponse response = _mapper.Map<GetByGidOtoBrandResponse>(otoBrand);
                return response;
            }
        }
    }
}
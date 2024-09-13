using Application.Features.DefinitionManagementFeatures.MeasureTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid
{
    public class GetByGidMeasureTypeQuery : IRequest<GetByGidMeasureTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidMeasureTypeQueryHandler : IRequestHandler<GetByGidMeasureTypeQuery, GetByGidMeasureTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
            private readonly MeasureTypeBusinessRules _measureTypeBusinessRules;

            public GetByGidMeasureTypeQueryHandler(IMapper mapper, IMeasureTypeReadRepository measureTypeReadRepository, MeasureTypeBusinessRules measureTypeBusinessRules)
            {
                _mapper = mapper;
                _measureTypeReadRepository = measureTypeReadRepository;
                _measureTypeBusinessRules = measureTypeBusinessRules;
            }

            public async Task<GetByGidMeasureTypeResponse> Handle(GetByGidMeasureTypeQuery request, CancellationToken cancellationToken)
            {
                X.MeasureType? measureType = await _measureTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _measureTypeBusinessRules.MeasureTypeShouldExistWhenSelected(measureType);

                GetByGidMeasureTypeResponse response = _mapper.Map<GetByGidMeasureTypeResponse>(measureType);
                return response;
            }
        }
    }
}
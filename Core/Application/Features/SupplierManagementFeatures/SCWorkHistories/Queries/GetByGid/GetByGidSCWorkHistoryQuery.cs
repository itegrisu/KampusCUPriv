using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid
{
    public class GetByGidSCWorkHistoryQuery : IRequest<GetByGidSCWorkHistoryResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCWorkHistoryQueryHandler : IRequestHandler<GetByGidSCWorkHistoryQuery, GetByGidSCWorkHistoryResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
            private readonly SCWorkHistoryBusinessRules _sCWorkHistoryBusinessRules;

            public GetByGidSCWorkHistoryQueryHandler(IMapper mapper, ISCWorkHistoryReadRepository sCWorkHistoryReadRepository, SCWorkHistoryBusinessRules sCWorkHistoryBusinessRules)
            {
                _mapper = mapper;
                _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
                _sCWorkHistoryBusinessRules = sCWorkHistoryBusinessRules;
            }

            public async Task<GetByGidSCWorkHistoryResponse> Handle(GetByGidSCWorkHistoryQuery request, CancellationToken cancellationToken)
            {
                X.SCWorkHistory? sCWorkHistory = await _sCWorkHistoryReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK));

                await _sCWorkHistoryBusinessRules.SCWorkHistoryShouldExistWhenSelected(sCWorkHistory);

                GetByGidSCWorkHistoryResponse response = _mapper.Map<GetByGidSCWorkHistoryResponse>(sCWorkHistory);
                return response;
            }
        }
    }
}
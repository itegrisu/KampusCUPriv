using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Rules;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid
{
    public class GetByGidSCCompanyQuery : IRequest<GetByGidSCCompanyResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCCompanyQueryHandler : IRequestHandler<GetByGidSCCompanyQuery, GetByGidSCCompanyResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
            private readonly SCCompanyBusinessRules _sCCompanyBusinessRules;

            public GetByGidSCCompanyQueryHandler(IMapper mapper, ISCCompanyReadRepository sCCompanyReadRepository, SCCompanyBusinessRules sCCompanyBusinessRules)
            {
                _mapper = mapper;
                _sCCompanyReadRepository = sCCompanyReadRepository;
                _sCCompanyBusinessRules = sCCompanyBusinessRules;
            }

            public async Task<GetByGidSCCompanyResponse> Handle(GetByGidSCCompanyQuery request, CancellationToken cancellationToken)
            {
                X.SCCompany? sCCompany = await _sCCompanyReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                //unutma
                //includes varsa eklenecek - Orn: Altta 
                await _sCCompanyBusinessRules.SCCompanyShouldExistWhenSelected(sCCompany);

                GetByGidSCCompanyResponse response = _mapper.Map<GetByGidSCCompanyResponse>(sCCompany);
                return response;
            }
        }
    }
}
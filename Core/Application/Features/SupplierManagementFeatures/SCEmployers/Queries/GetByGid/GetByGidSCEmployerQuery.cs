using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Rules;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid
{
    public class GetByGidSCEmployerQuery : IRequest<GetByGidSCEmployerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCEmployerQueryHandler : IRequestHandler<GetByGidSCEmployerQuery, GetByGidSCEmployerResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
            private readonly SCEmployerBusinessRules _sCEmployerBusinessRules;

            public GetByGidSCEmployerQueryHandler(IMapper mapper, ISCEmployerReadRepository sCEmployerReadRepository, SCEmployerBusinessRules sCEmployerBusinessRules)
            {
                _mapper = mapper;
                _sCEmployerReadRepository = sCEmployerReadRepository;
                _sCEmployerBusinessRules = sCEmployerBusinessRules;
            }

            public async Task<GetByGidSCEmployerResponse> Handle(GetByGidSCEmployerQuery request, CancellationToken cancellationToken)
            {
                X.SCEmployer? sCEmployer = await _sCEmployerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK));


                await _sCEmployerBusinessRules.SCEmployerShouldExistWhenSelected(sCEmployer);

                GetByGidSCEmployerResponse response = _mapper.Map<GetByGidSCEmployerResponse>(sCEmployer);
                return response;
            }
        }
    }
}
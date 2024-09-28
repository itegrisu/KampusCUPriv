using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Rules;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid
{
    public class GetByGidSCBankQuery : IRequest<GetByGidSCBankResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCBankQueryHandler : IRequestHandler<GetByGidSCBankQuery, GetByGidSCBankResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCBankReadRepository _sCBankReadRepository;
            private readonly SCBankBusinessRules _sCBankBusinessRules;

            public GetByGidSCBankQueryHandler(IMapper mapper, ISCBankReadRepository sCBankReadRepository, SCBankBusinessRules sCBankBusinessRules)
            {
                _mapper = mapper;
                _sCBankReadRepository = sCBankReadRepository;
                _sCBankBusinessRules = sCBankBusinessRules;
            }

            public async Task<GetByGidSCBankResponse> Handle(GetByGidSCBankQuery request, CancellationToken cancellationToken)
            {
                X.SCBank? sCBank = await _sCBankReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CurrencyFK));
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _sCBankBusinessRules.SCBankShouldExistWhenSelected(sCBank);

                GetByGidSCBankResponse response = _mapper.Map<GetByGidSCBankResponse>(sCBank);
                return response;
            }
        }
    }
}
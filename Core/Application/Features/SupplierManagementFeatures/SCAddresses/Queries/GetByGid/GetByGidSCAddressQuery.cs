using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Rules;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid
{
    public class GetByGidSCAddressQuery : IRequest<GetByGidSCAddressResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCAddressQueryHandler : IRequestHandler<GetByGidSCAddressQuery, GetByGidSCAddressResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCAddressReadRepository _sCAddressReadRepository;
            private readonly SCAddressBusinessRules _sCAddressBusinessRules;

            public GetByGidSCAddressQueryHandler(IMapper mapper, ISCAddressReadRepository sCAddressReadRepository, SCAddressBusinessRules sCAddressBusinessRules)
            {
                _mapper = mapper;
                _sCAddressReadRepository = sCAddressReadRepository;
                _sCAddressBusinessRules = sCAddressBusinessRules;
            }

            public async Task<GetByGidSCAddressResponse> Handle(GetByGidSCAddressQuery request, CancellationToken cancellationToken)
            {
                X.SCAddress? sCAddress = await _sCAddressReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CityFK));

                await _sCAddressBusinessRules.SCAddressShouldExistWhenSelected(sCAddress);

                GetByGidSCAddressResponse response = _mapper.Map<GetByGidSCAddressResponse>(sCAddress);
                return response;
            }
        }
    }
}
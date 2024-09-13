using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid
{
    public class GetByGidPersonnelAddressQuery : IRequest<GetByGidPersonnelAddressResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelAddressQueryHandler : IRequestHandler<GetByGidPersonnelAddressQuery, GetByGidPersonnelAddressResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelAddressReadRepository _personnelAddressReadRepository;
            private readonly PersonnelAddressBusinessRules _personnelAddressBusinessRules;

            public GetByGidPersonnelAddressQueryHandler(IMapper mapper, IPersonnelAddressReadRepository personnelAddressReadRepository, PersonnelAddressBusinessRules personnelAddressBusinessRules)
            {
                _mapper = mapper;
                _personnelAddressReadRepository = personnelAddressReadRepository;
                _personnelAddressBusinessRules = personnelAddressBusinessRules;
            }

            public async Task<GetByGidPersonnelAddressResponse> Handle(GetByGidPersonnelAddressQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelAddress? personnelAddress = await _personnelAddressReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: i => i.Include(x => x.UserFK).Include(x => x.CityFK));

                await _personnelAddressBusinessRules.PersonnelAddressShouldExistWhenSelected(personnelAddress);

                GetByGidPersonnelAddressResponse response = _mapper.Map<GetByGidPersonnelAddressResponse>(personnelAddress);
                return response;
            }
        }
    }
}
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Rules;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid
{
    public class GetByGidSCPersonnelQuery : IRequest<GetByGidSCPersonnelResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidSCPersonnelQueryHandler : IRequestHandler<GetByGidSCPersonnelQuery, GetByGidSCPersonnelResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
            private readonly SCPersonnelBusinessRules _sCPersonnelBusinessRules;

            public GetByGidSCPersonnelQueryHandler(IMapper mapper, ISCPersonnelReadRepository sCPersonnelReadRepository, SCPersonnelBusinessRules sCPersonnelBusinessRules)
            {
                _mapper = mapper;
                _sCPersonnelReadRepository = sCPersonnelReadRepository;
                _sCPersonnelBusinessRules = sCPersonnelBusinessRules;
            }

            public async Task<GetByGidSCPersonnelResponse> Handle(GetByGidSCPersonnelQuery request, CancellationToken cancellationToken)
            {
                X.SCPersonnel? sCPersonnel = await _sCPersonnelReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _sCPersonnelBusinessRules.SCPersonnelShouldExistWhenSelected(sCPersonnel);

                GetByGidSCPersonnelResponse response = _mapper.Map<GetByGidSCPersonnelResponse>(sCPersonnel);
                return response;
            }
        }
    }
}
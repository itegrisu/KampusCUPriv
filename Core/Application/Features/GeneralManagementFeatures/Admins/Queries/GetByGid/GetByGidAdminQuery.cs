using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.GeneralFeatures.Admins.Rules;
using Application.Repositories.GeneralManagementRepo.AdminRepo;

namespace Application.Features.GeneralFeatures.Admins.Queries.GetByGid
{
    public class GetByGidAdminQuery : IRequest<GetByGidAdminResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidAdminQueryHandler : IRequestHandler<GetByGidAdminQuery, GetByGidAdminResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAdminReadRepository _adminReadRepository;
            private readonly AdminBusinessRules _adminBusinessRules;

            public GetByGidAdminQueryHandler(IMapper mapper, IAdminReadRepository adminReadRepository, AdminBusinessRules adminBusinessRules)
            {
                _mapper = mapper;
                _adminReadRepository = adminReadRepository;
                _adminBusinessRules = adminBusinessRules;
            }

            public async Task<GetByGidAdminResponse> Handle(GetByGidAdminQuery request, CancellationToken cancellationToken)
            {
                X.Admin? admin = await _adminReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _adminBusinessRules.AdminShouldExistWhenSelected(admin);

                GetByGidAdminResponse response = _mapper.Map<GetByGidAdminResponse>(admin);
                return response;
            }
        }
    }
}
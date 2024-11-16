using Application.Features.VehicleManagementFeatures.Tyres.Rules;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid
{
    public class GetByGidTyreQuery : IRequest<GetByGidTyreResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTyreQueryHandler : IRequestHandler<GetByGidTyreQuery, GetByGidTyreResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITyreReadRepository _tyreReadRepository;
            private readonly TyreBusinessRules _tyreBusinessRules;

            public GetByGidTyreQueryHandler(IMapper mapper, ITyreReadRepository tyreReadRepository, TyreBusinessRules tyreBusinessRules)
            {
                _mapper = mapper;
                _tyreReadRepository = tyreReadRepository;
                _tyreBusinessRules = tyreBusinessRules;
            }

            public async Task<GetByGidTyreResponse> Handle(GetByGidTyreQuery request, CancellationToken cancellationToken)
            {
                X.Tyre? tyre = await _tyreReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TyreTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _tyreBusinessRules.TyreShouldExistWhenSelected(tyre);

                GetByGidTyreResponse response = _mapper.Map<GetByGidTyreResponse>(tyre);
                return response;
            }
        }
    }
}
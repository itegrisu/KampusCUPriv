using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Features.DefinationManagementFeatures.Warehouses.Rules;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid
{
    public class GetByGidWarehouseQuery : IRequest<GetByGidWarehouseResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidWarehouseQueryHandler : IRequestHandler<GetByGidWarehouseQuery, GetByGidWarehouseResponse>
        {
            private readonly IMapper _mapper;
            private readonly IWarehouseReadRepository _warehouseReadRepository;
            private readonly WarehouseBusinessRules _warehouseBusinessRules;

            public GetByGidWarehouseQueryHandler(IMapper mapper, IWarehouseReadRepository warehouseReadRepository, WarehouseBusinessRules warehouseBusinessRules)
            {
                _mapper = mapper;
                _warehouseReadRepository = warehouseReadRepository;
                _warehouseBusinessRules = warehouseBusinessRules;
            }

            public async Task<GetByGidWarehouseResponse> Handle(GetByGidWarehouseQuery request, CancellationToken cancellationToken)
            {
                Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);

                GetByGidWarehouseResponse response = _mapper.Map<GetByGidWarehouseResponse>(warehouse);
                return response;
            }
        }
    }
}
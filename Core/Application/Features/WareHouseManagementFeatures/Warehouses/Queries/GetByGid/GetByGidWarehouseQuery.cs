using Application.Features.WarehouseManagementFeatures.Warehouses.Rules;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetByGid
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
                X.Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));

                await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);

                GetByGidWarehouseResponse response = _mapper.Map<GetByGidWarehouseResponse>(warehouse);
                return response;
            }
        }
    }
}
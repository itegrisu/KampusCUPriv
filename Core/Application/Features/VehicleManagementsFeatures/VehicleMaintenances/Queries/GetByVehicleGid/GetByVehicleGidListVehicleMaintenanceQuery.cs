using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;


namespace Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleMaintenanceQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleMaintenanceQueryHandler : IRequestHandler<GetByVehicleGidListVehicleMaintenanceQuery, GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto>>
        {
            private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleMaintenance, GetByVehicleGidListVehicleMaintenanceListItemDto> _noPagination;

            public GetByVehicleGidListVehicleMaintenanceQueryHandler(IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository, IMapper mapper, NoPagination<X.VehicleMaintenance, GetByVehicleGidListVehicleMaintenanceListItemDto> noPagination)
            {
                _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto>> Handle(GetByVehicleGidListVehicleMaintenanceQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleMaintenance, object>>[]
                        {
                       x => x.VehicleAllFK,
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleMaintenance> vehicleMaintenances = await _vehicleMaintenanceReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto>>(vehicleMaintenances);
                return response;
            }
        }
    }
}

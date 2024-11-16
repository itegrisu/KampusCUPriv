using Application.Helpers.PaginationHelpers;
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

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetList;

public class GetListVehicleMaintenanceQuery : IRequest<GetListResponse<GetListVehicleMaintenanceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleMaintenanceQueryHandler : IRequestHandler<GetListVehicleMaintenanceQuery, GetListResponse<GetListVehicleMaintenanceListItemDto>>
    {
        private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleMaintenance, GetListVehicleMaintenanceListItemDto> _noPagination;

        public GetListVehicleMaintenanceQueryHandler(IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository, IMapper mapper, NoPagination<X.VehicleMaintenance, GetListVehicleMaintenanceListItemDto> noPagination)
        {
            _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleMaintenanceListItemDto>> Handle(GetListVehicleMaintenanceQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleMaintenance, object>>[]
                    {
                       x => x.VehicleAllFK,
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleMaintenance> vehicleMaintenances = await _vehicleMaintenanceReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleMaintenanceListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleMaintenanceListItemDto>>(vehicleMaintenances);
            return response;
        }
    }
}
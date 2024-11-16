using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetList;

public class GetListVehicleEquipmentQuery : IRequest<GetListResponse<GetListVehicleEquipmentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleEquipmentQueryHandler : IRequestHandler<GetListVehicleEquipmentQuery, GetListResponse<GetListVehicleEquipmentListItemDto>>
    {
        private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleEquipment, GetListVehicleEquipmentListItemDto> _noPagination;

        public GetListVehicleEquipmentQueryHandler(IVehicleEquipmentReadRepository vehicleEquipmentReadRepository, IMapper mapper, NoPagination<X.VehicleEquipment, GetListVehicleEquipmentListItemDto> noPagination)
        {
            _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleEquipmentListItemDto>> Handle(GetListVehicleEquipmentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleEquipment, object>>[]
                    {
                       x => x.VehicleAllFK,
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleEquipment> vehicleEquipments = await _vehicleEquipmentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleEquipmentListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleEquipmentListItemDto>>(vehicleEquipments);
            return response;
        }
    }
}
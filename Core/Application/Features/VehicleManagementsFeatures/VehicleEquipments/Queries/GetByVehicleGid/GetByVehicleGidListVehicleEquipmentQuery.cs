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

namespace Application.Features.VehicleManagementsFeatures.VehicleEquipments.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleEquipmentQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleEquipmentQueryHandler : IRequestHandler<GetByVehicleGidListVehicleEquipmentQuery, GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto>>
        {
            private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleEquipment, GetByVehicleGidListVehicleEquipmentListItemDto> _noPagination;

            public GetByVehicleGidListVehicleEquipmentQueryHandler(IVehicleEquipmentReadRepository vehicleEquipmentReadRepository, IMapper mapper, NoPagination<X.VehicleEquipment, GetByVehicleGidListVehicleEquipmentListItemDto> noPagination)
            {
                _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto>> Handle(GetByVehicleGidListVehicleEquipmentQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleEquipment, object>>[]
                        {
                           x => x.VehicleAllFK,
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleEquipment> vehicleEquipments = await _vehicleEquipmentReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto>>(vehicleEquipments);
                return response;
            }
        }
    }
}

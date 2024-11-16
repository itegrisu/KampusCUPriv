using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;


namespace Application.Features.VehicleManagementsFeatures.VehicleInspections.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleInspectionQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleInspectionQueryHandler : IRequestHandler<GetByVehicleGidListVehicleInspectionQuery, GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto>>
        {
            private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleInspection, GetByVehicleGidListVehicleInspectionListItemDto> _noPagination;

            public GetByVehicleGidListVehicleInspectionQueryHandler(IVehicleInspectionReadRepository vehicleInspectionReadRepository, IMapper mapper, NoPagination<X.VehicleInspection, GetByVehicleGidListVehicleInspectionListItemDto> noPagination)
            {
                _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto>> Handle(GetByVehicleGidListVehicleInspectionQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleInspection, object>>[]
                        {
                       x => x.VehicleAllFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleInspection> vehicleInspections = await _vehicleInspectionReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto>>(vehicleInspections);
                return response;
            }
        }
    }
}

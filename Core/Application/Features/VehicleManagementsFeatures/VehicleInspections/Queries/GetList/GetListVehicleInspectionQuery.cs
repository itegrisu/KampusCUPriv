using Application.Helpers.PaginationHelpers;
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

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetList;

public class GetListVehicleInspectionQuery : IRequest<GetListResponse<GetListVehicleInspectionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleInspectionQueryHandler : IRequestHandler<GetListVehicleInspectionQuery, GetListResponse<GetListVehicleInspectionListItemDto>>
    {
        private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleInspection, GetListVehicleInspectionListItemDto> _noPagination;

        public GetListVehicleInspectionQueryHandler(IVehicleInspectionReadRepository vehicleInspectionReadRepository, IMapper mapper, NoPagination<X.VehicleInspection, GetListVehicleInspectionListItemDto> noPagination)
        {
            _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleInspectionListItemDto>> Handle(GetListVehicleInspectionQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleInspection, object>>[]
                    {
                       x => x.VehicleAllFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleInspection> vehicleInspections = await _vehicleInspectionReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleInspectionListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleInspectionListItemDto>>(vehicleInspections);
            return response;
        }
    }
}
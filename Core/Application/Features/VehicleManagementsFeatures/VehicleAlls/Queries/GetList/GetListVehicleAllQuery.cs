using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetList;

public class GetListVehicleAllQuery : IRequest<GetListResponse<GetListVehicleAllListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleAllQueryHandler : IRequestHandler<GetListVehicleAllQuery, GetListResponse<GetListVehicleAllListItemDto>>
    {
        private readonly IVehicleAllReadRepository _vehicleAllReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleAll, GetListVehicleAllListItemDto> _noPagination;

        public GetListVehicleAllQueryHandler(IVehicleAllReadRepository vehicleAllReadRepository, IMapper mapper, NoPagination<X.VehicleAll, GetListVehicleAllListItemDto> noPagination)
        {
            _vehicleAllReadRepository = vehicleAllReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleAllListItemDto>> Handle(GetListVehicleAllQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)

                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleAll, object>>[]
                    {
                       x=> x.OtoBrandFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleAll> vehicleAlls = await _vehicleAllReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OtoBrandFK)
            );

            GetListResponse<GetListVehicleAllListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleAllListItemDto>>(vehicleAlls);
            return response;
        }
    }
}
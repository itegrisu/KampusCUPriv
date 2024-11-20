using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetList;

public class GetListVehicleRequestQuery : IRequest<GetListResponse<GetListVehicleRequestListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleRequestQueryHandler : IRequestHandler<GetListVehicleRequestQuery, GetListResponse<GetListVehicleRequestListItemDto>>
    {
        private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleRequest, GetListVehicleRequestListItemDto> _noPagination;

        public GetListVehicleRequestQueryHandler(IVehicleRequestReadRepository vehicleRequestReadRepository, IMapper mapper, NoPagination<X.VehicleRequest, GetListVehicleRequestListItemDto> noPagination)
        {
            _vehicleRequestReadRepository = vehicleRequestReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleRequestListItemDto>> Handle(GetListVehicleRequestQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleRequest, object>>[]
                    {
                       x => x.VehicleAllFK,
                       x => x.RequestUserFK,
                       x => x.ApprovedUserFK,
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleRequest> vehicleRequests = await _vehicleRequestReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK).Include(x => x.RequestUserFK).Include(x => x.ApprovedUserFK)
            );

            GetListResponse<GetListVehicleRequestListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleRequestListItemDto>>(vehicleRequests);
            return response;
        }
    }
}
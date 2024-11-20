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

namespace Application.Features.VehicleManagementsFeatures.VehicleRequests.Queries.GetByUserGid
{
    public class GetByUserGidListVehicleRequestQuery : IRequest<GetListResponse<GetByUserGidListVehicleRequestListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListVehicleRequestQueryHandler : IRequestHandler<GetByUserGidListVehicleRequestQuery, GetListResponse<GetByUserGidListVehicleRequestListItemDto>>
        {
            private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleRequest, GetByUserGidListVehicleRequestListItemDto> _noPagination;

            public GetByUserGidListVehicleRequestQueryHandler(IVehicleRequestReadRepository vehicleRequestReadRepository, IMapper mapper, NoPagination<X.VehicleRequest, GetByUserGidListVehicleRequestListItemDto> noPagination)
            {
                _vehicleRequestReadRepository = vehicleRequestReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListVehicleRequestListItemDto>> Handle(GetByUserGidListVehicleRequestQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        includes: new Expression<Func<VehicleRequest, object>>[]
                        {
                       x => x.VehicleAllFK,
                       x => x.RequestUserFK,
                       x => x.ApprovedUserFK,
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleRequest> vehicleRequests = await _vehicleRequestReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK).Include(x => x.RequestUserFK).Include(x => x.ApprovedUserFK)
                );

                GetListResponse<GetByUserGidListVehicleRequestListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListVehicleRequestListItemDto>>(vehicleRequests);
                return response;
            }
        }
    }
}

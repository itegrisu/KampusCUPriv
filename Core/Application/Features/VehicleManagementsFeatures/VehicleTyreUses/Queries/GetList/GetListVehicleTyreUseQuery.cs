using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetList;

public class GetListVehicleTyreUseQuery : IRequest<GetListResponse<GetListVehicleTyreUseListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleTyreUseQueryHandler : IRequestHandler<GetListVehicleTyreUseQuery, GetListResponse<GetListVehicleTyreUseListItemDto>>
    {
        private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleTyreUse, GetListVehicleTyreUseListItemDto> _noPagination;

        public GetListVehicleTyreUseQueryHandler(IVehicleTyreUseReadRepository vehicleTyreUseReadRepository, IMapper mapper, NoPagination<X.VehicleTyreUse, GetListVehicleTyreUseListItemDto> noPagination)
        {
            _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleTyreUseListItemDto>> Handle(GetListVehicleTyreUseQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleTyreUse, object>>[]
                    {
                       x => x.VehicleAllFK,
                       x=> x.TyreFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleTyreUse> vehicleTyreUses = await _vehicleTyreUseReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK).Include(x => x.TyreFK)
            );

            GetListResponse<GetListVehicleTyreUseListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleTyreUseListItemDto>>(vehicleTyreUses);
            return response;
        }
    }
}
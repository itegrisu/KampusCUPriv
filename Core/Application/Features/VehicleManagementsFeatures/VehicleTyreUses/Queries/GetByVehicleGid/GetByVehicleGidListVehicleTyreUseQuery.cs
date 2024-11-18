using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementsFeatures.VehicleTyreUses.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleTyreUseQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleTyreUseQueryHandler : IRequestHandler<GetByVehicleGidListVehicleTyreUseQuery, GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto>>
        {
            private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTyreUse, GetByVehicleGidListVehicleTyreUseListItemDto> _noPagination;

            public GetByVehicleGidListVehicleTyreUseQueryHandler(IVehicleTyreUseReadRepository vehicleTyreUseReadRepository, IMapper mapper, NoPagination<X.VehicleTyreUse, GetByVehicleGidListVehicleTyreUseListItemDto> noPagination)
            {
                _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto>> Handle(GetByVehicleGidListVehicleTyreUseQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleTyreUse, object>>[]
                        {
                           x => x.VehicleAllFK,
                           x=> x.TyreFK,
                           X => X.TyreFK.TyreTypeFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleTyreUse> vehicleTyreUses = await _vehicleTyreUseReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK).Include(x => x.TyreFK).Include(x => x.TyreFK).ThenInclude(x => x.TyreTypeFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto>>(vehicleTyreUses);
                return response;
            }
        }
    }
}

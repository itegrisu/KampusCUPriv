using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
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

namespace Application.Features.VehicleManagementsFeatures.Tyres.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleTyreQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleTyreListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid VehicleGid { get; set; }
        public class GetByVehicleGidListTyreQueryHandler : IRequestHandler<GetByVehicleGidListVehicleTyreQuery, GetListResponse<GetByVehicleGidListVehicleTyreListItemDto>>
        {
            private readonly IVehicleTyreReadRepository _tyreReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTyre, GetByVehicleGidListVehicleTyreListItemDto> _noPagination;

            public GetByVehicleGidListTyreQueryHandler(IVehicleTyreReadRepository tyreReadRepository, IMapper mapper, NoPagination<X.VehicleTyre, GetByVehicleGidListVehicleTyreListItemDto> noPagination)
            {
                _tyreReadRepository = tyreReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleTyreListItemDto>> Handle(GetByVehicleGidListVehicleTyreQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTyreTypeFK == request.VehicleGid,
                        includes: new Expression<Func<VehicleTyre, object>>[]
                        {
                       x => x.TyreTypeFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleTyre> tyres = await _tyreReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.TyreTypeFK)
                );

                GetListResponse<GetByVehicleGidListVehicleTyreListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleTyreListItemDto>>(tyres);
                return response;
            }
        }
    }
}

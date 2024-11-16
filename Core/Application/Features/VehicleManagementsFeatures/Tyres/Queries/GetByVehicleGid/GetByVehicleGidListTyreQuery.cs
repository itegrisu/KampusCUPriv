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
    public class GetByVehicleGidListTyreQuery : IRequest<GetListResponse<GetByVehicleGidListTyreListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid VehicleGid { get; set; }
        public class GetByVehicleGidListTyreQueryHandler : IRequestHandler<GetByVehicleGidListTyreQuery, GetListResponse<GetByVehicleGidListTyreListItemDto>>
        {
            private readonly ITyreReadRepository _tyreReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Tyre, GetByVehicleGidListTyreListItemDto> _noPagination;

            public GetByVehicleGidListTyreQueryHandler(ITyreReadRepository tyreReadRepository, IMapper mapper, NoPagination<X.Tyre, GetByVehicleGidListTyreListItemDto> noPagination)
            {
                _tyreReadRepository = tyreReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListTyreListItemDto>> Handle(GetByVehicleGidListTyreQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTyreTypeFK == request.VehicleGid,
                        includes: new Expression<Func<Tyre, object>>[]
                        {
                       x => x.TyreTypeFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.Tyre> tyres = await _tyreReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.TyreTypeFK)
                );

                GetListResponse<GetByVehicleGidListTyreListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListTyreListItemDto>>(tyres);
                return response;
            }
        }
    }
}

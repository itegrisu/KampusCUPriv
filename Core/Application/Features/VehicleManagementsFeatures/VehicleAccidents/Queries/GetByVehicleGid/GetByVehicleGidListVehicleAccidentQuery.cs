using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
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

namespace Application.Features.VehicleManagementsFeatures.VehicleAccidents.Queries.GetByUserGid
{
    public class GetByVehicleGidListVehicleAccidentQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleAccidentQueryHandler : IRequestHandler<GetByVehicleGidListVehicleAccidentQuery, GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto>>
        {
            private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleAccident, GetByVehicleGidListVehicleAccidentListItemDto> _noPagination;

            public GetByVehicleGidListVehicleAccidentQueryHandler(IVehicleAccidentReadRepository vehicleAccidentReadRepository, IMapper mapper, NoPagination<X.VehicleAccident, GetByVehicleGidListVehicleAccidentListItemDto> noPagination)
            {
                _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto>> Handle(GetByVehicleGidListVehicleAccidentQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleAccident, object>>[]
                        {
                       x => x.VehicleAllFK,
                        });

                IPaginate<X.VehicleAccident> vehicleAccidents = await _vehicleAccidentReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto>>(vehicleAccidents);
                return response;
            }
        }
    }
}

using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
using Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetList;

public class GetListVehicleAccidentQuery : IRequest<GetListResponse<GetListVehicleAccidentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleAccidentQueryHandler : IRequestHandler<GetListVehicleAccidentQuery, GetListResponse<GetListVehicleAccidentListItemDto>>
    {
        private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleAccident, GetListVehicleAccidentListItemDto> _noPagination;

        public GetListVehicleAccidentQueryHandler(IVehicleAccidentReadRepository vehicleAccidentReadRepository, IMapper mapper, NoPagination<X.VehicleAccident, GetListVehicleAccidentListItemDto> noPagination)
        {
            _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleAccidentListItemDto>> Handle(GetListVehicleAccidentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleAccident, object>>[]
                    {
                       x => x.VehicleAllFK,
                    });
            IPaginate<X.VehicleAccident> vehicleAccidents = await _vehicleAccidentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleAccidentListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleAccidentListItemDto>>(vehicleAccidents);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetList;

public class GetListVehicleInsuranceQuery : IRequest<GetListResponse<GetListVehicleInsuranceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleInsuranceQueryHandler : IRequestHandler<GetListVehicleInsuranceQuery, GetListResponse<GetListVehicleInsuranceListItemDto>>
    {
        private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleInsurance, GetListVehicleInsuranceListItemDto> _noPagination;

        public GetListVehicleInsuranceQueryHandler(IVehicleInsuranceReadRepository vehicleInsuranceReadRepository, IMapper mapper, NoPagination<X.VehicleInsurance, GetListVehicleInsuranceListItemDto> noPagination)
        {
            _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleInsuranceListItemDto>> Handle(GetListVehicleInsuranceQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleInsurance, object>>[]
                    {
                       x => x.VehicleAllFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleInsurance> vehicleInsurances = await _vehicleInsuranceReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleInsuranceListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleInsuranceListItemDto>>(vehicleInsurances);
            return response;
        }
    }
}
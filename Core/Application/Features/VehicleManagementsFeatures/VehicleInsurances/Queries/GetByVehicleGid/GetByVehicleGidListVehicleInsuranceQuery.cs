using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
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


namespace Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleInsuranceQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleInsuranceQueryHandler : IRequestHandler<GetByVehicleGidListVehicleInsuranceQuery, GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto>>
        {
            private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleInsurance, GetByVehicleGidListVehicleInsuranceListItemDto> _noPagination;

            public GetByVehicleGidListVehicleInsuranceQueryHandler(IVehicleInsuranceReadRepository vehicleInsuranceReadRepository, IMapper mapper, NoPagination<X.VehicleInsurance, GetByVehicleGidListVehicleInsuranceListItemDto> noPagination)
            {
                _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto>> Handle(GetByVehicleGidListVehicleInsuranceQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleInsurance, object>>[]
                        {
                       x => x.VehicleAllFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleInsurance> vehicleInsurances = await _vehicleInsuranceReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto>>(vehicleInsurances);
                return response;
            }
        }
    }
}

using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetHistoriesByVehicleGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetListWithDateRange
{
    public class GetListWithDateRangeVehicleTransactionQuery : IRequest<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class GetListWithDateRangeVehicleTransactionQueryHandler : IRequestHandler<GetListWithDateRangeVehicleTransactionQuery, GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>
        {
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTransaction, GetListWithDateRangeVehicleTransactionListItemDto> _noPagination;

            public GetListWithDateRangeVehicleTransactionQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetListWithDateRangeVehicleTransactionListItemDto> noPagination)
            {
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }
            public async Task<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>> Handle(GetListWithDateRangeVehicleTransactionQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)

                    return await _noPagination.NoPaginationAllData(cancellationToken,
                        predicate: x => x.CreatedDate >= request.StartDate && x.EndDate <= request.EndDate && x.DataState == DataState.Active && (x.VehicleStatus == EnumVehicleStatus.FirmaAraci || x.VehicleStatus == EnumVehicleStatus.KiralikAlinanArac),
                        includes: new Expression<Func<VehicleTransaction, object>>[]
                        {
                           x => x.UserFK,
                           x=> x.SCCompanyFK,
                           x=> x.VehicleAllFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleTransaction> vehicleTransactions = await _vehicleTransactionReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.CreatedDate >= request.StartDate && x.EndDate <= request.EndDate && x.DataState == DataState.Active,
                    include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK).Include(x => x.VehicleAllFK)
                );

                GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto> response = _mapper.Map<GetListResponse<GetListWithDateRangeVehicleTransactionListItemDto>>(vehicleTransactions);
                return response;
            }
        }
    }

}

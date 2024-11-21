using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetByVehicleTransactionGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
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

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetHistoriesByVehicleGid
{
    public class GetHistoriesByVehicleGidListVehicleTransactionQuery : IRequest<GetListResponse<GetHistoriesByVehicleGidListVehicleTransactionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicle { get; set; }
        public class GetHistoriesByVehicleGidListVehicleTransactionQueryHandler : IRequestHandler<GetHistoriesByVehicleGidListVehicleTransactionQuery, GetListResponse<GetHistoriesByVehicleGidListVehicleTransactionListItemDto>>
        {
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTransaction, GetHistoriesByVehicleGidListVehicleTransactionListItemDto> _noPagination;

            public GetHistoriesByVehicleGidListVehicleTransactionQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetHistoriesByVehicleGidListVehicleTransactionListItemDto> noPagination)
            {
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetHistoriesByVehicleGidListVehicleTransactionListItemDto>> Handle(GetHistoriesByVehicleGidListVehicleTransactionQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)

                    return await _noPagination.NoPaginationAllData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicle && x.DataState == DataState.Archive,
                        includes: new Expression<Func<VehicleTransaction, object>>[]
                        {
                           x => x.UserFK,
                           x => x.SCCompanyFK,
                           x => x.VehicleAllFK,
                           x => x.CurrencyFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleTransaction> vehicleTransactions = await _vehicleTransactionReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidVehicleFK == request.GidVehicle && x.DataState == DataState.Archive,
                    include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK).Include(x => x.VehicleAllFK).Include(x => x.CurrencyFK)
                );

                GetListResponse<GetHistoriesByVehicleGidListVehicleTransactionListItemDto> response = _mapper.Map<GetListResponse<GetHistoriesByVehicleGidListVehicleTransactionListItemDto>>(vehicleTransactions);
                return response;
            }
        }
    }

}

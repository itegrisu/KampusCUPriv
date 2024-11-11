using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Request;
using Core.Application.Responses;
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

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetByVehicleTransactionGid
{
    public class GetByVehicleTransactionGidListVehicleTransactionQuery : IRequest<GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByVehicleTransactionGidListVehicleTransactionQueryHandler : IRequestHandler<GetByVehicleTransactionGidListVehicleTransactionQuery, GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto>>
        {
            private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleTransaction, GetByVehicleTransactionGidListVehicleTransactionListItemDto> _noPagination;

            public GetByVehicleTransactionGidListVehicleTransactionQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetByVehicleTransactionGidListVehicleTransactionListItemDto> noPagination)
            {
                _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto>> Handle(GetByVehicleTransactionGidListVehicleTransactionQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)

                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.Gid == request.Gid,
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
                    predicate: x => x.Gid == request.Gid,
                    include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK).Include(x => x.VehicleAllFK)
                );

                GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto>>(vehicleTransactions);
                return response;
            }
        }
    }
}

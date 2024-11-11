using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetList;

public class GetListVehicleTransactionQuery : IRequest<GetListResponse<GetListVehicleTransactionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleTransactionQueryHandler : IRequestHandler<GetListVehicleTransactionQuery, GetListResponse<GetListVehicleTransactionListItemDto>>
    {
        private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleTransaction, GetListVehicleTransactionListItemDto> _noPagination;

        public GetListVehicleTransactionQueryHandler(IVehicleTransactionReadRepository vehicleTransactionReadRepository, IMapper mapper, NoPagination<X.VehicleTransaction, GetListVehicleTransactionListItemDto> noPagination)
        {
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleTransactionListItemDto>> Handle(GetListVehicleTransactionQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)

                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleTransaction, object>>[]
                    {
                       x => x.UserFK,
                       x=> x.SCCompanyFK,
                       x=> x.VehicleAllFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleTransaction> vehicleTransactions = await _vehicleTransactionReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK).Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListVehicleTransactionListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleTransactionListItemDto>>(vehicleTransactions);
            return response;
        }
    }
}
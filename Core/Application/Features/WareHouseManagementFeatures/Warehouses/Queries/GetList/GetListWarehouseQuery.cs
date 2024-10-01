using Application.Helpers.PaginationHelpers;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.WarehouseManagements;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Queries.GetList;

public class GetListWarehouseQuery : IRequest<GetListResponse<GetListWarehouseListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListWarehouseQueryHandler : IRequestHandler<GetListWarehouseQuery, GetListResponse<GetListWarehouseListItemDto>>
    {
        private readonly IWarehouseReadRepository _warehouseReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Warehouse, GetListWarehouseListItemDto> _noPagination;

        public GetListWarehouseQueryHandler(IWarehouseReadRepository warehouseReadRepository, IMapper mapper, NoPagination<X.Warehouse, GetListWarehouseListItemDto> noPagination)
        {
            _warehouseReadRepository = warehouseReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListWarehouseListItemDto>> Handle(GetListWarehouseQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {

                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Warehouse, object>>[]
                    {
                       x=>x.OrganizationFK
                    });
            }

            IPaginate<X.Warehouse> warehouses = await _warehouseReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OrganizationFK)
            );

            GetListResponse<GetListWarehouseListItemDto> response = _mapper.Map<GetListResponse<GetListWarehouseListItemDto>>(warehouses);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetList;

public class GetListWarehouseQuery : IRequest<GetListResponse<GetListWarehouseListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListWarehouseQueryHandler : IRequestHandler<GetListWarehouseQuery, GetListResponse<GetListWarehouseListItemDto>>
    {
        private readonly IWarehouseReadRepository _warehouseReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<Warehouse, GetListWarehouseListItemDto> _noPagination;

        public GetListWarehouseQueryHandler(IWarehouseReadRepository warehouseReadRepository, IMapper mapper, NoPagination<Warehouse, GetListWarehouseListItemDto> noPagination)
        {
            _warehouseReadRepository = warehouseReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListWarehouseListItemDto>> Handle(GetListWarehouseQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<Warehouse, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.WarehouseMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<Warehouse> warehouses = await _warehouseReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWarehouseListItemDto> response = _mapper.Map<GetListResponse<GetListWarehouseListItemDto>>(warehouses);
            return response;
        }
    }
}
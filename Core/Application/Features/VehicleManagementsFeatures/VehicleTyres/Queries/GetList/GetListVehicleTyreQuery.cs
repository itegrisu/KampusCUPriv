using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Queries.GetList;

public class GetListVehicleTyreQuery : IRequest<GetListResponse<GetListVehicleTyreListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTyreQueryHandler : IRequestHandler<GetListVehicleTyreQuery, GetListResponse<GetListVehicleTyreListItemDto>>
    {
        private readonly IVehicleTyreReadRepository _tyreReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleTyre, GetListVehicleTyreListItemDto> _noPagination;

        public GetListTyreQueryHandler(IVehicleTyreReadRepository tyreReadRepository, IMapper mapper, NoPagination<X.VehicleTyre, GetListVehicleTyreListItemDto> noPagination)
        {
            _tyreReadRepository = tyreReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleTyreListItemDto>> Handle(GetListVehicleTyreQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleTyre, object>>[]
                    {
                       x => x.TyreTypeFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleTyre> tyres = await _tyreReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.TyreTypeFK)
            );

            GetListResponse<GetListVehicleTyreListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleTyreListItemDto>>(tyres);
            return response;
        }
    }
}
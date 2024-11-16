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

public class GetListTyreQuery : IRequest<GetListResponse<GetListTyreListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTyreQueryHandler : IRequestHandler<GetListTyreQuery, GetListResponse<GetListTyreListItemDto>>
    {
        private readonly ITyreReadRepository _tyreReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Tyre, GetListTyreListItemDto> _noPagination;

        public GetListTyreQueryHandler(ITyreReadRepository tyreReadRepository, IMapper mapper, NoPagination<X.Tyre, GetListTyreListItemDto> noPagination)
        {
            _tyreReadRepository = tyreReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTyreListItemDto>> Handle(GetListTyreQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Tyre, object>>[]
                    {
                       x => x.TyreTypeFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.Tyre> tyres = await _tyreReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.TyreTypeFK)
            );

            GetListResponse<GetListTyreListItemDto> response = _mapper.Map<GetListResponse<GetListTyreListItemDto>>(tyres);
            return response;
        }
    }
}
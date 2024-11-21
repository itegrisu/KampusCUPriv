using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetList;

public class GetListDistrictQuery : IRequest<GetListResponse<GetListDistrictListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListDistrictQueryHandler : IRequestHandler<GetListDistrictQuery, GetListResponse<GetListDistrictListItemDto>>
    {
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.District, GetListDistrictListItemDto> _noPagination;

        public GetListDistrictQueryHandler(IDistrictReadRepository districtReadRepository, IMapper mapper, NoPagination<X.District, GetListDistrictListItemDto> noPagination)
        {
            _districtReadRepository = districtReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListDistrictListItemDto>> Handle(GetListDistrictQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<District, object>>[]
                    {
                       x => x.CityFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.District> districts = await _districtReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.CityFK)
            );

            GetListResponse<GetListDistrictListItemDto> response = _mapper.Map<GetListResponse<GetListDistrictListItemDto>>(districts);
            return response;
        }
    }
}
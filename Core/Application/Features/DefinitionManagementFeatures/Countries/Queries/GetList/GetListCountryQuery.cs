using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetList;

public class GetListCountryQuery : IRequest<GetListResponse<GetListCountryListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCountryQueryHandler : IRequestHandler<GetListCountryQuery, GetListResponse<GetListCountryListItemDto>>
    {
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Country, GetListCountryListItemDto> _noPagination;

        public GetListCountryQueryHandler(ICountryReadRepository countryReadRepository, IMapper mapper, NoPagination<X.Country, GetListCountryListItemDto> noPagination)
        {
            _countryReadRepository = countryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListCountryListItemDto>> Handle(GetListCountryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.Country> countrys = await _countryReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCountryListItemDto> response = _mapper.Map<GetListResponse<GetListCountryListItemDto>>(countrys);
            return response;
        }
    }
}
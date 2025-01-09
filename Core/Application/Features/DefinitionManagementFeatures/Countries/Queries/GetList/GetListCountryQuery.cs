using Application.Abstractions;
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
        private readonly IUlas�mService _ulas�mService;

        public GetListCountryQueryHandler(ICountryReadRepository countryReadRepository, IMapper mapper, NoPagination<X.Country, GetListCountryListItemDto> noPagination, IUlas�mService ulas�mService)
        {
            _countryReadRepository = countryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _ulas�mService = ulas�mService;
        }

        public async Task<GetListResponse<GetListCountryListItemDto>> Handle(GetListCountryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.RowNo);


            await _ulas�mService.TestService();

            IPaginate<X.Country> countrys = await _countryReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: x => x.OrderBy(x => x.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCountryListItemDto> response = _mapper.Map<GetListResponse<GetListCountryListItemDto>>(countrys);
            return response;
        }
    }
}
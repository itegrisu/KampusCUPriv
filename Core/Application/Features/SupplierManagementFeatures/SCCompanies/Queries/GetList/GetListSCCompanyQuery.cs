using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetList;

public class GetListSCCompanyQuery : IRequest<GetListResponse<GetListSCCompanyListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSCCompanyQueryHandler : IRequestHandler<GetListSCCompanyQuery, GetListResponse<GetListSCCompanyListItemDto>>
    {
        private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.SCCompany, GetListSCCompanyListItemDto> _noPagination;

        public GetListSCCompanyQueryHandler(ISCCompanyReadRepository sCCompanyReadRepository, IMapper mapper, NoPagination<X.SCCompany, GetListSCCompanyListItemDto> noPagination)
        {
            _sCCompanyReadRepository = sCCompanyReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListSCCompanyListItemDto>> Handle(GetListSCCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.SCCompany> sCCompanys = await _sCCompanyReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSCCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListSCCompanyListItemDto>>(sCCompanys);
            return response;
        }
    }
}
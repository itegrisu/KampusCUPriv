using Application.Helpers.PaginationHelpers;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System.Linq.Expressions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetList;

public class GetListSCCompanyQuery : IRequest<GetListResponse<GetListSCCompanyListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public bool? IsHotel { get; set; }

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
            Expression<Func<X.SCCompany, bool>> predicate = x => x.IsHotel == true || x.IsHotel == false;
            if (request.IsHotel == true)
                predicate = x => x.IsHotel == true;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, predicate: predicate);

            IPaginate<X.SCCompany> sCCompanys = await _sCCompanyReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSCCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListSCCompanyListItemDto>>(sCCompanys);
            return response;
        }
    }
}
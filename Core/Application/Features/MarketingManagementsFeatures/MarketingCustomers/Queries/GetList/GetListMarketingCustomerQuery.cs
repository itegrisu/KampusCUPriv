using Application.Helpers.PaginationHelpers;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetList;

public class GetListMarketingCustomerQuery : IRequest<GetListResponse<GetListMarketingCustomerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListMarketingCustomerQueryHandler : IRequestHandler<GetListMarketingCustomerQuery, GetListResponse<GetListMarketingCustomerListItemDto>>
    {
        private readonly IMarketingCustomerReadRepository _marketingCustomerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.MarketingCustomer, GetListMarketingCustomerListItemDto> _noPagination;

        public GetListMarketingCustomerQueryHandler(IMarketingCustomerReadRepository marketingCustomerReadRepository, IMapper mapper, NoPagination<X.MarketingCustomer, GetListMarketingCustomerListItemDto> noPagination)
        {
            _marketingCustomerReadRepository = marketingCustomerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListMarketingCustomerListItemDto>> Handle(GetListMarketingCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<MarketingCustomer, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.MarketingCustomerMembers
                //    });
                return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.MarketingCustomer> marketingCustomers = await _marketingCustomerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMarketingCustomerListItemDto> response = _mapper.Map<GetListResponse<GetListMarketingCustomerListItemDto>>(marketingCustomers);
            return response;
        }
    }
}
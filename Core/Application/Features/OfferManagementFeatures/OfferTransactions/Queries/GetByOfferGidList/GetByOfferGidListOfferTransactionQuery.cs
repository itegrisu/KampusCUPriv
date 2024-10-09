using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByOfferGidList
{
    public class GetByOfferGidListOfferTransactionQuery : IRequest<GetListResponse<GetByOfferGidListOfferTransactionListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid offerGid { get; set; }

        public class GetByOfferGidListOfferTransactionQueryHandler : IRequestHandler<GetByOfferGidListOfferTransactionQuery, GetListResponse<GetByOfferGidListOfferTransactionListItemDto>>
        {
            private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<OfferTransaction, GetByOfferGidListOfferTransactionListItemDto> _noPagination;

            public GetByOfferGidListOfferTransactionQueryHandler(IOfferTransactionReadRepository offerTransactionReadRepository, IMapper mapper, NoPagination<OfferTransaction, GetByOfferGidListOfferTransactionListItemDto> noPagination)
            {
                _offerTransactionReadRepository = offerTransactionReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByOfferGidListOfferTransactionListItemDto>> Handle(GetByOfferGidListOfferTransactionQuery request, CancellationToken cancellationToken)
            {
                //if (request.PageIndex == -1)
                //{

                //    return await _noPagination.NoPaginationData(cancellationToken,
                //       predicate: x => x.OfferFK.Gid == request.offerGid,
                //       orderBy: x => x.CreatedDate,
                //       includes: new Expression<Func<OfferTransaction, object>>[]
                //       {
                //           x=>x.CurrencyFK,
                //           x=>x.OfferFK
                //       });
                //}

                IPaginate<OfferTransaction> offerTransactions = await _offerTransactionReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.CurrencyFK).Include(x => x.OfferFK),
                    predicate: x => x.OfferFK.Gid == request.offerGid,
                     orderBy: x => x.OrderByDescending(x => x.CreatedDate)
                );

                GetListResponse<GetByOfferGidListOfferTransactionListItemDto> response = _mapper.Map<GetListResponse<GetByOfferGidListOfferTransactionListItemDto>>(offerTransactions);
                return response;
            }
        }
    }
}

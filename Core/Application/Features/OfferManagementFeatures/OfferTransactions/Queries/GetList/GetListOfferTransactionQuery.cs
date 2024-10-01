using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetList;

public class GetListOfferTransactionQuery : IRequest<GetListResponse<GetListOfferTransactionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOfferTransactionQueryHandler : IRequestHandler<GetListOfferTransactionQuery, GetListResponse<GetListOfferTransactionListItemDto>>
    {
        private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OfferTransaction, GetListOfferTransactionListItemDto> _noPagination;

        public GetListOfferTransactionQueryHandler(IOfferTransactionReadRepository offerTransactionReadRepository, IMapper mapper, NoPagination<X.OfferTransaction, GetListOfferTransactionListItemDto> noPagination)
        {
            _offerTransactionReadRepository = offerTransactionReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOfferTransactionListItemDto>> Handle(GetListOfferTransactionQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<OfferTransaction, object>>[]
                   {
                       x=>x.CurrencyFK,
                       x=>x.OfferFK
                   });
            }

            IPaginate<X.OfferTransaction> offerTransactions = await _offerTransactionReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.CurrencyFK).Include(x => x.OfferFK)
            );

            GetListResponse<GetListOfferTransactionListItemDto> response = _mapper.Map<GetListResponse<GetListOfferTransactionListItemDto>>(offerTransactions);
            return response;
        }
    }
}
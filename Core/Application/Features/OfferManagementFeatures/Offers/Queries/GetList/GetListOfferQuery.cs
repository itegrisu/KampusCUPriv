using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Queries.GetList;

public class GetListOfferQuery : IRequest<GetListResponse<GetListOfferListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOfferQueryHandler : IRequestHandler<GetListOfferQuery, GetListResponse<GetListOfferListItemDto>>
    {
        private readonly IOfferReadRepository _offerReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Offer, GetListOfferListItemDto> _noPagination;

        public GetListOfferQueryHandler(IOfferReadRepository offerReadRepository, IMapper mapper, NoPagination<X.Offer, GetListOfferListItemDto> noPagination)
        {
            _offerReadRepository = offerReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOfferListItemDto>> Handle(GetListOfferQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.Offer> offers = await _offerReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOfferListItemDto> response = _mapper.Map<GetListResponse<GetListOfferListItemDto>>(offers);
            return response;
        }
    }
}
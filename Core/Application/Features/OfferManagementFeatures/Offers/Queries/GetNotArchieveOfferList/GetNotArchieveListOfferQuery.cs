using Application.Helpers.PaginationHelpers;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OfferManagements;
using MediatR;

namespace Application.Features.OfferManagementFeatures.Offers.Queries.GetNotArchieveOfferList
{
    public class GetNotArchieveListOfferQuery : IRequest<GetListResponse<GetNotArchieveListOfferListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetNotArchieveListOfferQueryHandler : IRequestHandler<GetNotArchieveListOfferQuery, GetListResponse<GetNotArchieveListOfferListItemDto>>
        {
            private readonly IOfferReadRepository _offerReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<Offer, GetNotArchieveListOfferListItemDto> _noPagination;

            public GetNotArchieveListOfferQueryHandler(IOfferReadRepository offerReadRepository, IMapper mapper, NoPagination<Offer, GetNotArchieveListOfferListItemDto> noPagination)
            {
                _offerReadRepository = offerReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetNotArchieveListOfferListItemDto>> Handle(GetNotArchieveListOfferQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.OfferStatus == Domain.Enums.EnumOfferStatus.Aktif || x.OfferStatus == Domain.Enums.EnumOfferStatus.Beklemede || x.OfferStatus == Domain.Enums.EnumOfferStatus.KabulEdildi);


                IPaginate<Offer> offers = await _offerReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    predicate: x => x.OfferStatus == Domain.Enums.EnumOfferStatus.Aktif || x.OfferStatus == Domain.Enums.EnumOfferStatus.Beklemede || x.OfferStatus == Domain.Enums.EnumOfferStatus.KabulEdildi,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetNotArchieveListOfferListItemDto> response = _mapper.Map<GetListResponse<GetNotArchieveListOfferListItemDto>>(offers);
                return response;
            }
        }
    }
}

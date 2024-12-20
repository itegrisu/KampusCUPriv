using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetList;

public class GetListReservationHotelQuery : IRequest<GetListResponse<GetListReservationHotelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListReservationHotelQueryHandler : IRequestHandler<GetListReservationHotelQuery, GetListResponse<GetListReservationHotelListItemDto>>
    {
        private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationHotel, GetListReservationHotelListItemDto> _noPagination;

        public GetListReservationHotelQueryHandler(IReservationHotelReadRepository reservationHotelReadRepository, IMapper mapper, NoPagination<X.ReservationHotel, GetListReservationHotelListItemDto> noPagination)
        {
            _reservationHotelReadRepository = reservationHotelReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationHotelListItemDto>> Handle(GetListReservationHotelQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<ReservationHotel, object>>[]
                    {
                       x => x.SCCompanyFK,
                       x=> x.BuyCurrencyFK,
                       x=> x.SellCurrencyFK,
                       x=> x.ReservationFK,
                    });
            IPaginate<X.ReservationHotel> reservationHotels = await _reservationHotelReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken, 
                include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK)
            );

            GetListResponse<GetListReservationHotelListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelListItemDto>>(reservationHotels);
            return response;
        }
    }
}
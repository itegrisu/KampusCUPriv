using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetList;

public class GetListReservationHotelQuery : IRequest<GetListResponse<GetListReservationHotelListItemDto>>
{
    public string ReservationGid { get; set; } // 0 hepsi
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;


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
            Expression<Func<ReservationHotel, bool>> predicate = null;
            if (request.ReservationGid != "0")
                predicate = x => x.GidReservationFK.ToString() == request.ReservationGid;


            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    includes: new Expression<Func<ReservationHotel, object>>[]
                    {
                       x => x.SCCompanyFK,
                       x=> x.BuyCurrencyFK,
                       x=> x.SellCurrencyFK,
                       x=> x.ReservationFK,
                    });
            IPaginate<X.ReservationHotel> reservationHotels = await _reservationHotelReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK)
            );

            GetListResponse<GetListReservationHotelListItemDto> response = _mapper.Map<GetListResponse<GetListReservationHotelListItemDto>>(reservationHotels);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetListByPartTime
{
    public class GetListByPartTimeReservationHotelQuery : IRequest<GetListResponse<GetListByPartTimeReservationHotelListItemDto>>
    {
        public string PartTimeGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetListByPartTimeWorkerReservationHotelQueryHandler : IRequestHandler<GetListByPartTimeReservationHotelQuery, GetListResponse<GetListByPartTimeReservationHotelListItemDto>>
        {
            private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<ReservationHotel, GetListByPartTimeReservationHotelListItemDto> _noPagination;

            public GetListByPartTimeWorkerReservationHotelQueryHandler(IReservationHotelReadRepository reservationHotelReadRepository, IMapper mapper, NoPagination<ReservationHotel, GetListByPartTimeReservationHotelListItemDto> noPagination)
            {
                _reservationHotelReadRepository = reservationHotelReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListByPartTimeReservationHotelListItemDto>> Handle(GetListByPartTimeReservationHotelQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<ReservationHotel, bool>> predicate = null;
                predicate = x => x.ReservationHotelPartTimeWorkers.Any(r => r.GidPartTimeWorkerFK.ToString() == request.PartTimeGid
                && r.DataState == DataState.Active && r.IsActive == true);


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

                IPaginate<ReservationHotel> reservationHotels = await _reservationHotelReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: predicate,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.SCCompanyFK).Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK)
                );

                GetListResponse<GetListByPartTimeReservationHotelListItemDto> response = _mapper.Map<GetListResponse<GetListByPartTimeReservationHotelListItemDto>>(reservationHotels);
                return response;
            }
        }
    }
}

using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetListByWorker
{
    public class GetListByWorkerReservationHotelQuery : IRequest<GetListResponse<GetListByWorkerReservationHotelListItemDto>>
    {
        public string WorkerGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetListByWorkerReservationHotelQueryHandler : IRequestHandler<GetListByWorkerReservationHotelQuery, GetListResponse<GetListByWorkerReservationHotelListItemDto>>
        {
            private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<ReservationHotel, GetListByWorkerReservationHotelListItemDto> _noPagination;

            public GetListByWorkerReservationHotelQueryHandler(IReservationHotelReadRepository reservationHotelReadRepository, IMapper mapper, NoPagination<ReservationHotel, GetListByWorkerReservationHotelListItemDto> noPagination)
            {
                _reservationHotelReadRepository = reservationHotelReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListByWorkerReservationHotelListItemDto>> Handle(GetListByWorkerReservationHotelQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<ReservationHotel, bool>> predicate = null;
                predicate = x => x.SCCompanyFK.ReservationHotelStaffs.Any(r => r.Gid.ToString() == request.WorkerGid
                && r.DataState == DataState.Active && r.HotelStaffStatus == EnumHotelStaffStatus.Aktif);


                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: predicate,
                        includes: new Expression<Func<ReservationHotel, object>>[]
                        {
                       x => x.SCCompanyFK,
                       x => x.SCCompanyFK.ReservationHotelStaffs,
                       x=> x.BuyCurrencyFK,
                       x=> x.SellCurrencyFK,
                       x=> x.ReservationFK,
                        });

                IPaginate<ReservationHotel> reservationHotels = await _reservationHotelReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: predicate,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.SCCompanyFK).Include(x => x.SCCompanyFK).ThenInclude(x => x.ReservationHotelStaffs)
                    .Include(X => X.BuyCurrencyFK).Include(x => x.SellCurrencyFK).Include(x => x.ReservationFK)
                );

                GetListResponse<GetListByWorkerReservationHotelListItemDto> response = _mapper.Map<GetListResponse<GetListByWorkerReservationHotelListItemDto>>(reservationHotels);
                return response;

            }
        }
    }
}
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Enum;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetList;

public class GetListReservationDetailQuery : IRequest<GetListResponse<GetListReservationDetailListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string ReservationHotelGid { get; set; }
    public bool IsPartTimeWorker { get; set; }
    public Guid PartTimeWorkerGid { get; set; }

    public class GetListReservationDetailQueryHandler : IRequestHandler<GetListReservationDetailQuery, GetListResponse<GetListReservationDetailListItemDto>>
    {
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> _noPagination;
        private readonly ReservationDetailBusinessRules _reservationDetailBusinessRules;

        public GetListReservationDetailQueryHandler(IReservationDetailReadRepository reservationDetailReadRepository, IMapper mapper, NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> noPagination, ReservationDetailBusinessRules reservationDetailBusinessRules)
        {
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _reservationDetailBusinessRules = reservationDetailBusinessRules;
        }

        public async Task<GetListResponse<GetListReservationDetailListItemDto>> Handle(GetListReservationDetailQuery request, CancellationToken cancellationToken)
        {
            await _reservationDetailBusinessRules.IsThereSelectedHotel(Guid.Parse(request.ReservationHotelGid));
            if (request.IsPartTimeWorker == true && request.PartTimeWorkerGid != null)
                await _reservationDetailBusinessRules.PartTimeWorkerControl(request.ReservationHotelGid, request.PartTimeWorkerGid);


            Expression<Func<ReservationDetail, bool>> predicate = null;
            if (request.ReservationHotelGid != null)
                predicate = x => x.GidReservationHotelFK.ToString() == request.ReservationHotelGid;

            if (request.PageIndex == -1)
            {
                var data = await _noPagination.NoPaginationData(cancellationToken,
                     predicate: predicate,
                     includes: new Expression<Func<ReservationDetail, object>>[]
                      {
                          x => x.ReservationHotelFK,
                          x => x.ReservationHotelFK.ReservationFK,
                          x => x.ReservationHotelFK.BuyCurrencyFK,
                          x => x.ReservationHotelFK.SellCurrencyFK,
                          x => x.RoomTypeFK,
                          x => x.ReservationHotelFK.SCCompanyFK,
                          x => x.AccommodationDates
                      });

                foreach (var item in data.Items)
                {
                    var reservationDetail = await _reservationDetailReadRepository
                        .GetWhere(x => x.Gid == item.Gid)
                        .Include(x => x.AccommodationDates.Where(ad => ad.DataState == DataState.Active))
                        .FirstOrDefaultAsync();

                    if (reservationDetail?.AccommodationDates != null)
                    {
                        // Oda bazýnda gruplandýrma ve dolu oda sayýsýný hesaplama
                        item.FullRoomCount = reservationDetail.AccommodationDates
                            .Where(ad => ad.DataState == DataState.Active) // Aktif kayýtlar
                            .GroupBy(ad => ad.GidRoomNoFK) // Odalara göre gruplandýr
                            .Count(); // Her odadan en az bir kiþi yerleþtirilmiþ oda sayýsýný al
                    }
                    else
                    {
                        item.FullRoomCount = 0;
                    }
                }

                return data;

            }


            IPaginate<X.ReservationDetail> reservationDetails = await _reservationDetailReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.ReservationFK)
                .Include(x => x.ReservationHotelFK).ThenInclude(x => x.BuyCurrencyFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.SellCurrencyFK)
                .Include(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK).Include(x => x.AccommodationDates)
            );

            var response = _mapper.Map<GetListResponse<GetListReservationDetailListItemDto>>(reservationDetails);

            // FullRoomCount hesaplanýyor 
            foreach (var item in reservationDetails.Items)
            {
                var mappedItem = response.Items.First(x => x.Gid == item.Gid);
                mappedItem.FullRoomCount = item.AccommodationDates?.Count ?? 0;
            }

            return response;
        }
    }
}
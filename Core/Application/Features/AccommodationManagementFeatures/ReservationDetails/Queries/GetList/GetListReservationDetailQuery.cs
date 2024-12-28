using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using AutoMapper;
using Core.Application.Responses;
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

    public class GetListReservationDetailQueryHandler : IRequestHandler<GetListReservationDetailQuery, GetListResponse<GetListReservationDetailListItemDto>>
    {
        private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> _noPagination;

        public GetListReservationDetailQueryHandler(IReservationDetailReadRepository reservationDetailReadRepository, IMapper mapper, NoPagination<X.ReservationDetail, GetListReservationDetailListItemDto> noPagination)
        {
            _reservationDetailReadRepository = reservationDetailReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListReservationDetailListItemDto>> Handle(GetListReservationDetailQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ReservationDetail, bool>> predicate = null;
            if (request.ReservationHotelGid != null)
                predicate = x => x.GidReservationHotelFK.ToString() == request.ReservationHotelGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    includes: new Expression<Func<ReservationDetail, object>>[]
                    {
                       x => x.ReservationHotelFK,
                       x => x.ReservationHotelFK.ReservationFK,
                       x => x.ReservationHotelFK.BuyCurrencyFK,
                       x => x.ReservationHotelFK.SellCurrencyFK,
                       x => x.RoomTypeFK,
                       x => x.ReservationHotelFK.SCCompanyFK
                    });
            IPaginate<X.ReservationDetail> reservationDetails = await _reservationDetailReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.ReservationFK)
                .Include(x => x.ReservationHotelFK).ThenInclude(x => x.BuyCurrencyFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.SellCurrencyFK)
                .Include(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK)
            );

            GetListResponse<GetListReservationDetailListItemDto> response = _mapper.Map<GetListResponse<GetListReservationDetailListItemDto>>(reservationDetails);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetListByHotel
{
    public class GetListByHotelAccommodationDateQuery : IRequest<GetListResponse<GetListByHotelAccommodationDateListItemDto>>
    {
        public Guid ReservationHotelGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetListByHotelAccommodationDateQueryHandler : IRequestHandler<GetListByHotelAccommodationDateQuery, GetListResponse<GetListByHotelAccommodationDateListItemDto>>
        {
            private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<AccommodationDate, GetListByHotelAccommodationDateListItemDto> _noPagination;

            public GetListByHotelAccommodationDateQueryHandler(IAccommodationDateReadRepository accommodationDateReadRepository, IMapper mapper, NoPagination<AccommodationDate, GetListByHotelAccommodationDateListItemDto> noPagination)
            {
                _accommodationDateReadRepository = accommodationDateReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListByHotelAccommodationDateListItemDto>> Handle(GetListByHotelAccommodationDateQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<AccommodationDate, bool>> predicate = x => x.ReservationDetailFK.ReservationHotelFK.Gid == request.ReservationHotelGid;

                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: predicate,
                        includes: new Expression<Func<AccommodationDate, object>>[]
                        {
                        x => x.GuestFK,
                        x => x.ReservationDetailFK,
                        x => x.ReservationRoomFK,
                        x => x.ReservationDetailFK.ReservationHotelFK,
                        x => x.ReservationDetailFK.ReservationHotelFK.SCCompanyFK,
                        x => x.ReservationDetailFK.RoomTypeFK
                        });
                }

                IPaginate<AccommodationDate> accommodationDates = await _accommodationDateReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: predicate,
                    cancellationToken: cancellationToken,
                    include: x => x
                        .Include(x => x.GuestFK)
                        .Include(x => x.ReservationDetailFK)
                        .Include(x => x.ReservationRoomFK)
                        .Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK)
                        .Include(x => x.ReservationDetailFK).ThenInclude(x => x.ReservationHotelFK)
                        .Include(x => x.ReservationDetailFK).ThenInclude(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK)
                );

                GetListResponse<GetListByHotelAccommodationDateListItemDto> response = _mapper.Map<GetListResponse<GetListByHotelAccommodationDateListItemDto>>(accommodationDates);
                return response;
            }
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;

public class GetListAccommodationDateQuery : IRequest<GetListResponse<GetListAccommodationDateListItemDto>>
{
    public string ReservationDetailGid { get; set; }
    public string ReservationRoomGid { get; set; }
    public DateTime Date { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetListAccommodationDateQueryHandler : IRequestHandler<GetListAccommodationDateQuery, GetListResponse<GetListAccommodationDateListItemDto>>
    {
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.AccommodationDate, GetListAccommodationDateListItemDto> _noPagination;

        public GetListAccommodationDateQueryHandler(IAccommodationDateReadRepository accommodationDateReadRepository, IMapper mapper, NoPagination<X.AccommodationDate, GetListAccommodationDateListItemDto> noPagination)
        {
            _accommodationDateReadRepository = accommodationDateReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAccommodationDateListItemDto>> Handle(GetListAccommodationDateQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AccommodationDate, bool>> predicate = null;
            if (request.ReservationDetailGid != null && request.ReservationRoomGid != null && request.Date != null)
                predicate = x => x.Date == request.Date && x.GidReservationDetailFK.ToString() == request.ReservationDetailGid && x.GidRoomNoFK.ToString() == request.ReservationRoomGid;


            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
                    includes: new Expression<Func<AccommodationDate, object>>[]
                    {
                       x => x.GuestFK,
                       x=> x.ReservationDetailFK,
                       x=> x.ReservationRoomFK,
                       x=>x.ReservationDetailFK.ReservationHotelFK,
                       x=>x.ReservationDetailFK.ReservationHotelFK.SCCompanyFK,
                    });
            IPaginate<X.AccommodationDate> accommodationDates = await _accommodationDateReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK).
                Include(x => x.ReservationDetailFK).ThenInclude(x => x.ReservationHotelFK).Include(x => x.ReservationDetailFK).ThenInclude(x => x.ReservationHotelFK).ThenInclude(x => x.SCCompanyFK)
            );

            GetListResponse<GetListAccommodationDateListItemDto> response = _mapper.Map<GetListResponse<GetListAccommodationDateListItemDto>>(accommodationDates);
            return response;
        }
    }
}
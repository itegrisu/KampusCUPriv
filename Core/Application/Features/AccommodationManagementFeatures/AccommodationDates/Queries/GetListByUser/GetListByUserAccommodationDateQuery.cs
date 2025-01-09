using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetListByUser
{
    public class GetListByUserAccommodationDateQuery : IRequest<GetListResponse<GetListByUserAccommodationDateListItemDto>>
    {
        public Guid UserGid { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public class GetListByUserAccommodationDateQueryHandler : IRequestHandler<GetListByUserAccommodationDateQuery, GetListResponse<GetListByUserAccommodationDateListItemDto>>
        {
            private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<AccommodationDate, GetListByUserAccommodationDateListItemDto> _noPagination;
            private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;

            public GetListByUserAccommodationDateQueryHandler(IAccommodationDateReadRepository accommodationDateReadRepository, IMapper mapper, NoPagination<AccommodationDate, GetListByUserAccommodationDateListItemDto> noPagination, AccommodationDateBusinessRules accommodationDateBusinessRules)
            {
                _accommodationDateReadRepository = accommodationDateReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _accommodationDateBusinessRules = accommodationDateBusinessRules;
            }

            public async Task<GetListResponse<GetListByUserAccommodationDateListItemDto>> Handle(GetListByUserAccommodationDateQuery request, CancellationToken cancellationToken)
            {
                await _accommodationDateBusinessRules.IsGuestFKExist(request.UserGid);

                Expression<Func<AccommodationDate, bool>> predicate = x => x.GidGuestFK == request.UserGid;

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

                GetListResponse<GetListByUserAccommodationDateListItemDto> response = _mapper.Map<GetListResponse<GetListByUserAccommodationDateListItemDto>>(accommodationDates);
                return response;


            }
        }
    }
}
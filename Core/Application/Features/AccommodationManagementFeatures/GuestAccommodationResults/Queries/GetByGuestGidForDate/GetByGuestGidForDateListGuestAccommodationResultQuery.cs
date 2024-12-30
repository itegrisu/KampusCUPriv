using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForDate
{
    public class GetByGuestGidForDateListGuestAccommodationResultQuery : IRequest<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GuestGid { get; set; }
        public class GetByGuestGidForDateListGuestAccommodationResultQueryHandler : IRequestHandler<GetByGuestGidForDateListGuestAccommodationResultQuery, GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>
        {
            private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
            private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationResult, GetByGuestGidForDateListGuestAccommodationResultListItemDto> _noPagination;

            public GetByGuestGidForDateListGuestAccommodationResultQueryHandler(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationResult, GetByGuestGidForDateListGuestAccommodationResultListItemDto> noPagination, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository)
            {
                _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
            }

            public async Task<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>> Handle(GetByGuestGidForDateListGuestAccommodationResultQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    // Tüm odaları al (dolu ve boş dahil)
                    var allRooms = await _guestAccommodationRoomReadRepository.GetListAsync(
                        predicate: x => x.GidGuestAccommodationFK == request.GuestGid,
                        include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK),
                        cancellationToken: cancellationToken
                    );

                    // Doluluk bilgilerini al
                    var list = await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GuestAccommodationPersonFK.GidGuestAccommodationFK == request.GuestGid,
                        includes: new Expression<Func<GuestAccommodationResult, object>>[]
                        {
                            x => x.GuestAccommodationPersonFK,
                            x => x.GuestAccommodationRoomFK.RoomTypeFK,
                        });

                    // Oda bazlı gruplama işlemi
                    var groupedRooms = allRooms.Items
                        .OrderBy(x => x.Date)
                        .GroupBy(x => x.Gid)
                        .Select(group =>
                        {
                            var matchingResults = list.Items
                                .Where(r => r.GidGuestAccommodationRoomFK == group.Key)
                                .ToList();

                            return new GetByGuestGidForDateListGuestAccommodationResultListItemDto
                            {
                                GidGuestAccommodationRoomFK = group.Key, // Oda ID
                                GuestAccommodationRoomFKRoomTypeFKName = group.First().RoomTypeFK.Name, // Oda Tipi
                                GuestAccommodationRoomFKRoomTypeFKCapacity = group.First().RoomTypeFK.Capacity, // Kapasite
                                Persons = matchingResults.Select(r => r.GuestAccommodationPersonFKFullName).ToList(), // Kişiler
                                Count = $"{matchingResults.Count}/{group.First().RoomTypeFK.Capacity}", // Kapasite Bilgisi
                                GuestAccommodationRoomFKDate = group.First().Date,
                                Gid = matchingResults.FirstOrDefault()?.Gid ?? Guid.Empty // Boş oda için varsayılan GID
                            };
                        }).ToList();

                    // DTO oluştur ve dönüş yap
                    var response1 = new GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>
                    {
                        Items = groupedRooms,
                        Index = 0,
                        Size = groupedRooms.Count,
                        Count = groupedRooms.Count,
                        Pages = 1,
                        HasPrevious = false,
                        HasNext = false
                    };

                    return response1;

                }


                IPaginate<X.GuestAccommodationResult> guestAccommodationResults = await _guestAccommodationResultReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GuestAccommodationPersonFK.GidGuestAccommodationFK == request.GuestGid,
                    include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK)
                );

                GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto> response = _mapper.Map<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>(guestAccommodationResults);
                return response;
            }
        }
    }
}

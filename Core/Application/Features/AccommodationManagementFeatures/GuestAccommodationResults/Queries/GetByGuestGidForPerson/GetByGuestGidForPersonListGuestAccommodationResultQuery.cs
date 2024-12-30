using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForDate;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using AutoMapper;
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

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForPerson
{
    public class GetByGuestGidForPersonListGuestAccommodationResultQuery : IRequest<GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GuestGid { get; set; }
        public class GetByGuestGidForPersonListGuestAccommodationResultQueryHandler : IRequestHandler<GetByGuestGidForPersonListGuestAccommodationResultQuery, GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto>>
        {
            private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationResult, GetByGuestGidForPersonListGuestAccommodationResultListItemDto> _noPagination;

            public GetByGuestGidForPersonListGuestAccommodationResultQueryHandler(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationResult, GetByGuestGidForPersonListGuestAccommodationResultListItemDto> noPagination)
            {
                _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto>> Handle(GetByGuestGidForPersonListGuestAccommodationResultQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    var list = await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GuestAccommodationPersonFK.GidGuestAccommodationFK == request.GuestGid,
                        includes: new Expression<Func<GuestAccommodationResult, object>>[]
                        {
                            x => x.GuestAccommodationPersonFK,
                            x => x.GuestAccommodationRoomFK.RoomTypeFK,
                        });

                    // Oda bazlı gruplama işlemi
                    var groupedPersons = list.Items
                        .OrderBy(x => x.GuestAccommodationRoomFKDate) // Tarihe göre sıralama
                        .GroupBy(x => x.GidGuestAccommodationPersonFK)
                        .Select(group => new GetByGuestGidForPersonListGuestAccommodationResultListItemDto
                        {
                            GidGuestAccommodationPersonFK = group.Key, // Kişi ID
                            GuestAccommodationPersonFKFullName = group.First().GuestAccommodationPersonFKFullName, // Kişi İsmi
                            AccommodationInfo = group.Select(g => $"{g.GuestAccommodationRoomFKDate:dd.MM.yyyy} - {g.GuestAccommodationRoomFKRoomTypeFKName}").ToList(), // Konaklama Bilgileri
                            TotalDateCount = group.Count(), // Toplam Gün Sayısı
                            Gid = group.First().Gid // İlgili Gid değeri
                        }).ToList();

                    // DTO oluştur ve dönüş yap
                    var response1 = new GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto>
                    {
                        Items = groupedPersons,
                        Index = 0,
                        Size = groupedPersons.Count,
                        Count = groupedPersons.Count,
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

                GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto> response = _mapper.Map<GetListResponse<GetByGuestGidForPersonListGuestAccommodationResultListItemDto>>(guestAccommodationResults);
                return response;
            }
        }
    }

}

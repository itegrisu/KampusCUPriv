using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestRepo;
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

namespace Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByLetter
{
    public class GetByLetterGetByLetterListGuestQuery : IRequest<GetListResponse<GetByLetterListGuestListItemDto>>
    {
        public string Letter { get; set; }

        public class GetByLetterGetByLetterListGuestQueryHandler : IRequestHandler<GetByLetterGetByLetterListGuestQuery, GetListResponse<GetByLetterListGuestListItemDto>>
        {
            private readonly IGuestReadRepository _guestReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Guest, GetByLetterListGuestListItemDto> _noPagination;

            public GetByLetterGetByLetterListGuestQueryHandler(IGuestReadRepository guestReadRepository, IMapper mapper, NoPagination<X.Guest, GetByLetterListGuestListItemDto> noPagination)
            {
                _guestReadRepository = guestReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByLetterListGuestListItemDto>> Handle(GetByLetterGetByLetterListGuestQuery request, CancellationToken cancellationToken)
            {
                // Kullanıcının girdiği sorguyu al ve kontrol et
                string query = request.Letter?.ToLower();

                // Eğer sorgu boş veya null ise predicate uygulanmaz (tüm veriler döner)
                Expression<Func<Guest, bool>>? predicate = null;

                if (!string.IsNullOrEmpty(query))
                {
                    predicate = x =>
                        (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(query)) ||
                        (!string.IsNullOrEmpty(x.Surename) && x.Surename.ToLower().Contains(query)) ||
                        (!string.IsNullOrEmpty(x.IdNumber) && x.IdNumber.ToLower().Contains(query)) ||
                        (!string.IsNullOrEmpty(x.HesCode) && x.HesCode.ToLower().Contains(query));
                }

                // NoPagination çağrısı (predicate null olabilir)
                return await _noPagination.NoPaginationData(
                    cancellationToken,
                    predicate: predicate, // Eğer predicate null ise filtreleme yapılmaz
                    includes: new Expression<Func<Guest, object>>[]
                    {
                     x => x.CountryFK, // İlişkili veriler
                    }
                );
            }


        }
    }
}

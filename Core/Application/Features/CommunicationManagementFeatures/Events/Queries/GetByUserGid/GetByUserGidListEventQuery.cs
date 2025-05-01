using Application.Features.CommunicationFeatures.Events.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationManagementFeatures.Events.Queries.GetByUserGid
{
    public class GetByUserGidListEventQuery : IRequest<GetListResponse<GetByUserGidListEventListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListEventQueryHandler : IRequestHandler<GetByUserGidListEventQuery, GetListResponse<GetByUserGidListEventListItemDto>>
        {
            private readonly IEventReadRepository _eventReadRepository;
            private readonly IStudentClubReadRepository _studentClubReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Event, GetByUserGidListEventListItemDto> _noPagination;

            public GetByUserGidListEventQueryHandler(IEventReadRepository eventReadRepository, IMapper mapper, NoPagination<X.Event, GetByUserGidListEventListItemDto> noPagination, IStudentClubReadRepository studentClubReadRepository)
            {
                _eventReadRepository = eventReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _studentClubReadRepository = studentClubReadRepository;
            }

            public async Task<GetListResponse<GetByUserGidListEventListItemDto>> Handle(GetByUserGidListEventQuery request, CancellationToken cancellationToken)
            {
                var userClubs = await _studentClubReadRepository.GetListAsync(
                    predicate: x => x.GidUserFK == request.UserGid && x.DataState == Core.Enum.DataState.Active,
                    include: x => x.Include(x => x.ClubFK),
                    cancellationToken: cancellationToken
                );

                var clubGids = userClubs.Items.Select(x => x.GidClubFK).ToList();

                if (request.PageIndex == -1)
                {
                    // Eğer tüm verileri filtreye göre döndürecekseniz
                    return await _noPagination.NoPaginationData(
                        cancellationToken: cancellationToken,
                        predicate: x => clubGids.Contains(x.GidClubFK) && x.EventStatus == Domain.Enums.EnumEventStatus.Active,
                        includes: new Expression<Func<Event, object>>[]
                        {
                           x => x.ClubFK
                        });
                }

                // Sayfalama işlemiyle dönecek veriler
                IPaginate<X.Event> events = await _eventReadRepository.GetListAsync(
                    predicate: x => clubGids.Contains(x.GidClubFK) && x.EventStatus == Domain.Enums.EnumEventStatus.Active, 
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.ClubFK)
                );

                GetListResponse<GetByUserGidListEventListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListEventListItemDto>>(events);
                return response;
            }

        }
    }
}

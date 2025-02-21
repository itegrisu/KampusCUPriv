using Application.Features.CommunicationFeatures.Events.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByUserGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.CommunicationManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationManagementFeatures.Events.Queries.GetByClubGid
{
    public class GetByClubGidListEventQuery : IRequest<GetListResponse<GetByClubGidListEventListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ClubGid { get; set; }
        public class GetByClubGidListEventQueryHandler : IRequestHandler<GetByClubGidListEventQuery, GetListResponse<GetByClubGidListEventListItemDto>>
        {
            private readonly IEventReadRepository _eventReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Event, GetByClubGidListEventListItemDto> _noPagination;

            public GetByClubGidListEventQueryHandler(IEventReadRepository eventReadRepository, IMapper mapper, NoPagination<X.Event, GetByClubGidListEventListItemDto> noPagination)
            {
                _eventReadRepository = eventReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByClubGidListEventListItemDto>> Handle(GetByClubGidListEventQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.EventStatus == EnumEventStatus.Active && x.GidClubFK == request.ClubGid ,
                        includes: new Expression<Func<Event, object>>[]
                        {
                       x => x.ClubFK,
                        });
                IPaginate<X.Event> events = await _eventReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.EventStatus == EnumEventStatus.Active && x.GidClubFK == request.ClubGid,
                    include: x => x.Include(x => x.ClubFK)
                );

                GetListResponse<GetByClubGidListEventListItemDto> response = _mapper.Map<GetListResponse<GetByClubGidListEventListItemDto>>(events);
                return response;
            }
        }
    }
}

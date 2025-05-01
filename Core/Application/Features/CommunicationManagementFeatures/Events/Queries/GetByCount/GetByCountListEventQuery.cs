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

namespace Application.Features.CommunicationManagementFeatures.Events.Queries.GetByCount
{
    public class GetByCountListEventQuery : IRequest<GetListResponse<GetByCountListEventListItemDto>>
    {
        public int Count { get; set; }
        public class GetByCountListEventQueryHandler : IRequestHandler<GetByCountListEventQuery, GetListResponse<GetByCountListEventListItemDto>>
        {
            private readonly IEventReadRepository _eventReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Event, GetByCountListEventListItemDto> _noPagination;

            public GetByCountListEventQueryHandler(IEventReadRepository eventReadRepository, IMapper mapper, NoPagination<X.Event, GetByCountListEventListItemDto> noPagination)
            {
                _eventReadRepository = eventReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCountListEventListItemDto>> Handle(GetByCountListEventQuery request, CancellationToken cancellationToken)
            {
                IPaginate<X.Event> events = await _eventReadRepository.GetListAsync(
                    index: 0,
                    size: request.Count,
                    cancellationToken: cancellationToken,
                    orderByDesc: x => x.OrderByDescending(e => e.CreatedDate),
                    include: x => x.Include(x => x.ClubFK),
                    predicate: x => x.EventStatus == EnumEventStatus.Active && x.DataState == Core.Enum.DataState.Active
                );

                GetListResponse<GetByCountListEventListItemDto> response = _mapper.Map<GetListResponse<GetByCountListEventListItemDto>>(events);
                return response;
            }
        }
    }
}

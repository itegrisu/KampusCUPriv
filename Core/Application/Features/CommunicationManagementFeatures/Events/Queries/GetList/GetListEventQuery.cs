using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.CommunicationManagements;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Events.Queries.GetList;

public class GetListEventQuery : IRequest<GetListResponse<GetListEventListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListEventQueryHandler : IRequestHandler<GetListEventQuery, GetListResponse<GetListEventListItemDto>>
    {
        private readonly IEventReadRepository _eventReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Event, GetListEventListItemDto> _noPagination;

        public GetListEventQueryHandler(IEventReadRepository eventReadRepository, IMapper mapper, NoPagination<X.Event, GetListEventListItemDto> noPagination)
        {
            _eventReadRepository = eventReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListEventListItemDto>> Handle(GetListEventQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: x=> x.EventStatus == EnumEventStatus.Active,
                    includes: new Expression<Func<Event, object>>[]
                    {
                       x => x.ClubFK,
                    });
            IPaginate<X.Event> events = await _eventReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                predicate: x => x.EventStatus == EnumEventStatus.Active,
                include: x => x.Include(x => x.ClubFK)
            );

            GetListResponse<GetListEventListItemDto> response = _mapper.Map<GetListResponse<GetListEventListItemDto>>(events);
            return response;
        }
    }
}
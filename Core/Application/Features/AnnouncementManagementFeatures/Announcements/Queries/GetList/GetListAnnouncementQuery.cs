using Application.Helpers.PaginationHelpers;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetList;

public class GetListAnnouncementQuery : IRequest<GetListResponse<GetListAnnouncementListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAnnouncementQueryHandler : IRequestHandler<GetListAnnouncementQuery, GetListResponse<GetListAnnouncementListItemDto>>
    {
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<Announcement, GetListAnnouncementListItemDto> _noPagination;

        public GetListAnnouncementQueryHandler(IAnnouncementReadRepository announcementReadRepository, IMapper mapper, NoPagination<Announcement, GetListAnnouncementListItemDto> noPagination)
        {
            _announcementReadRepository = announcementReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAnnouncementListItemDto>> Handle(GetListAnnouncementQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.RowNo);

            IPaginate<Announcement> announcements = await _announcementReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: o => o.OrderBy(o => o.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListAnnouncementListItemDto>>(announcements);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetList;

public class GetListAnnouncementQuery : IRequest<GetListResponse<GetListAnnouncementListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAnnouncementQueryHandler : IRequestHandler<GetListAnnouncementQuery, GetListResponse<GetListAnnouncementListItemDto>>
    {
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Announcement, GetListAnnouncementListItemDto> _noPagination;

        public GetListAnnouncementQueryHandler(IAnnouncementReadRepository announcementReadRepository, IMapper mapper, NoPagination<X.Announcement, GetListAnnouncementListItemDto> noPagination)
        {
            _announcementReadRepository = announcementReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAnnouncementListItemDto>> Handle(GetListAnnouncementQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Announcement, object>>[]
                    {
                       x => x.ClubFK,
                    });
            IPaginate<X.Announcement> announcements = await _announcementReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ClubFK)
            );

            GetListResponse<GetListAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListAnnouncementListItemDto>>(announcements);
            return response;
        }
    }
}
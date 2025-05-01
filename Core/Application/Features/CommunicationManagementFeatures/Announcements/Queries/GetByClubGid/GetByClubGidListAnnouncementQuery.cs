using Application.Features.CommunicationFeatures.Announcements.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
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

namespace Application.Features.CommunicationManagementFeatures.Announcements.Queries.GetByClubGid
{
    public class GetByClubGidListAnnouncementQuery : IRequest<GetListResponse<GetByClubGidListAnnouncementListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ClubGid { get; set; }
        public class GetByClubGidListAnnouncementQueryHandler : IRequestHandler<GetByClubGidListAnnouncementQuery, GetListResponse<GetByClubGidListAnnouncementListItemDto>>
        {
            private readonly IAnnouncementReadRepository _announcementReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Announcement, GetByClubGidListAnnouncementListItemDto> _noPagination;

            public GetByClubGidListAnnouncementQueryHandler(IAnnouncementReadRepository announcementReadRepository, IMapper mapper, NoPagination<X.Announcement, GetByClubGidListAnnouncementListItemDto> noPagination)
            {
                _announcementReadRepository = announcementReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByClubGidListAnnouncementListItemDto>> Handle(GetByClubGidListAnnouncementQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidClubFK == request.ClubGid && x.DataState == Core.Enum.DataState.Active,
                        includes: new Expression<Func<Announcement, object>>[]
                        {
                       x => x.ClubFK,
                        });
                IPaginate<X.Announcement> announcements = await _announcementReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidClubFK == request.ClubGid && x.DataState == Core.Enum.DataState.Active,
                    include: x => x.Include(x => x.ClubFK)
                );

                GetListResponse<GetByClubGidListAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetByClubGidListAnnouncementListItemDto>>(announcements);
                return response;
            }
        }
    }
}

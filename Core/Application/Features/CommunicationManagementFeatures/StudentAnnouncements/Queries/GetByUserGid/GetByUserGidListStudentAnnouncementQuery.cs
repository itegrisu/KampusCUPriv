using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
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

namespace Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Queries.GetByUserGid
{
    public class GetByUserGidListStudentAnnouncementQuery : IRequest<GetListResponse<GetByUserGidListStudentAnnouncementListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListStudentAnnouncementQueryHandler : IRequestHandler<GetByUserGidListStudentAnnouncementQuery, GetListResponse<GetByUserGidListStudentAnnouncementListItemDto>>
        {
            private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.StudentAnnouncement, GetByUserGidListStudentAnnouncementListItemDto> _noPagination;

            public GetByUserGidListStudentAnnouncementQueryHandler(IStudentAnnouncementReadRepository studentAnnouncementReadRepository, IMapper mapper, NoPagination<X.StudentAnnouncement, GetByUserGidListStudentAnnouncementListItemDto> noPagination)
            {
                _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListStudentAnnouncementListItemDto>> Handle(GetByUserGidListStudentAnnouncementQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidUserFK == request.UserGid && x.DataState == Core.Enum.DataState.Active,
                        includes: new Expression<Func<StudentAnnouncement, object>>[]
                        {
                          x => x.UserFK,
                          x=> x.AnnouncementFK,
                          x=> x.AnnouncementFK.ClubFK
                        });
                IPaginate<X.StudentAnnouncement> studentAnnouncements = await _studentAnnouncementReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.AnnouncementFK).ThenInclude(x => x.ClubFK).Include(x => x.AnnouncementFK),
                    predicate: x => x.GidUserFK == request.UserGid && x.DataState == Core.Enum.DataState.Active
                );

                GetListResponse<GetByUserGidListStudentAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListStudentAnnouncementListItemDto>>(studentAnnouncements);
                return response;
            }
        }
    }
}

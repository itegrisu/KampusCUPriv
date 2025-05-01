using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementQuery : IRequest<GetListResponse<GetListStudentAnnouncementListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListStudentAnnouncementQueryHandler : IRequestHandler<GetListStudentAnnouncementQuery, GetListResponse<GetListStudentAnnouncementListItemDto>>
    {
        private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.StudentAnnouncement, GetListStudentAnnouncementListItemDto> _noPagination;

        public GetListStudentAnnouncementQueryHandler(IStudentAnnouncementReadRepository studentAnnouncementReadRepository, IMapper mapper, NoPagination<X.StudentAnnouncement, GetListStudentAnnouncementListItemDto> noPagination)
        {
            _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStudentAnnouncementListItemDto>> Handle(GetListStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<StudentAnnouncement, object>>[]
                    {
                       x => x.UserFK,
                       x => x.AnnouncementFK,
                       x => x.AnnouncementFK.ClubFK
                    });
            IPaginate<X.StudentAnnouncement> studentAnnouncements = await _studentAnnouncementReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.AnnouncementFK).ThenInclude(x => x.ClubFK).Include(x => x.AnnouncementFK)
            );

            GetListResponse<GetListStudentAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListStudentAnnouncementListItemDto>>(studentAnnouncements);
            return response;
        }
    }
}
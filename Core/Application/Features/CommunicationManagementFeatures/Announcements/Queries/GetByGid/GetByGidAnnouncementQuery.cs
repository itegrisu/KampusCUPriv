using AutoMapper;
using MediatR;
using X = Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.CommunicationFeatures.Announcements.Rules;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid
{
    public class GetByGidAnnouncementQuery : IRequest<GetByGidAnnouncementResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidAnnouncementQueryHandler : IRequestHandler<GetByGidAnnouncementQuery, GetByGidAnnouncementResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAnnouncementReadRepository _announcementReadRepository;
            private readonly AnnouncementBusinessRules _announcementBusinessRules;

            public GetByGidAnnouncementQueryHandler(IMapper mapper, IAnnouncementReadRepository announcementReadRepository, AnnouncementBusinessRules announcementBusinessRules)
            {
                _mapper = mapper;
                _announcementReadRepository = announcementReadRepository;
                _announcementBusinessRules = announcementBusinessRules;
            }

            public async Task<GetByGidAnnouncementResponse> Handle(GetByGidAnnouncementQuery request, CancellationToken cancellationToken)
            {
                X.Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClubFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(announcement);

                GetByGidAnnouncementResponse response = _mapper.Map<GetByGidAnnouncementResponse>(announcement);
                return response;
            }
        }
    }
}
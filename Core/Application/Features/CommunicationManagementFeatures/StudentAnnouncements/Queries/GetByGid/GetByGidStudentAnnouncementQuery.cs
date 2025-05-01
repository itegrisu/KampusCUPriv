using AutoMapper;
using MediatR;
using X = Domain.Entities.CommunicationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid
{
    public class GetByGidStudentAnnouncementQuery : IRequest<GetByGidStudentAnnouncementResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStudentAnnouncementQueryHandler : IRequestHandler<GetByGidStudentAnnouncementQuery, GetByGidStudentAnnouncementResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
            private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

            public GetByGidStudentAnnouncementQueryHandler(IMapper mapper, IStudentAnnouncementReadRepository studentAnnouncementReadRepository, StudentAnnouncementBusinessRules studentAnnouncementBusinessRules)
            {
                _mapper = mapper;
                _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
                _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
            }

            public async Task<GetByGidStudentAnnouncementResponse> Handle(GetByGidStudentAnnouncementQuery request, CancellationToken cancellationToken)
            {
                X.StudentAnnouncement? studentAnnouncement = await _studentAnnouncementReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.AnnouncementFK).ThenInclude(x => x.ClubFK).Include(x => x.AnnouncementFK)
);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);

                GetByGidStudentAnnouncementResponse response = _mapper.Map<GetByGidStudentAnnouncementResponse>(studentAnnouncement);
                return response;
            }
        }
    }
}
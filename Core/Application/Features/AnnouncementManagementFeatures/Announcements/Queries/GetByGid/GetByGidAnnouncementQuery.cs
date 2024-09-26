using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;

public class GetByGidAnnouncementQuery : IRequest<GetByGidAnnouncementResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAnnouncementQueryHandler : IRequestHandler<GetByGidAnnouncementQuery, GetByGidAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;

        public GetByIdAnnouncementQueryHandler(IMapper mapper, IAnnouncementReadRepository announcementReadRepository, AnnouncementBusinessRules announcementBusinessRules)
        {
            _mapper = mapper;
            _announcementReadRepository = announcementReadRepository;
            _announcementBusinessRules = announcementBusinessRules;
        }

        public async Task<GetByGidAnnouncementResponse> Handle(GetByGidAnnouncementQuery request, CancellationToken cancellationToken)
        {
            await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(request.Gid.ToString());

            Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: a => a.Gid == request.Gid, cancellationToken: cancellationToken);

            GetByGidAnnouncementResponse response = _mapper.Map<GetByGidAnnouncementResponse>(announcement);
            return response;
        }
    }
}
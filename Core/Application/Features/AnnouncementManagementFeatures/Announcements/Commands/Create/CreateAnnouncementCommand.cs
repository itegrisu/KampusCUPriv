using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using Core.Enum;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Create;

public class CreateAnnouncementCommand : IRequest<CreatedAnnouncementResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Link { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public ShowType ShowType { get; set; }
    public int RowNo { get; set; }

    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, CreatedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;

        public CreateAnnouncementCommandHandler(IMapper mapper, IAnnouncementWriteRepository announcementWriteRepository,
                                         AnnouncementBusinessRules announcementBusinessRules, IAnnouncementReadRepository announcementReadRepository)
        {
            _mapper = mapper;
            _announcementWriteRepository = announcementWriteRepository;
            _announcementBusinessRules = announcementBusinessRules;
            _announcementReadRepository = announcementReadRepository;
        }

        public async Task<CreatedAnnouncementResponse> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            Announcement announcement = _mapper.Map<Announcement>(request);

            await _announcementWriteRepository.AddAsync(announcement);
            await _announcementWriteRepository.SaveAsync();

            Announcement SavedAnnouncement = await _announcementReadRepository.GetAsync(predicate: x => x.Gid == announcement.Gid);
            GetByGidAnnouncementResponse obj = _mapper.Map<GetByGidAnnouncementResponse>(SavedAnnouncement);

            return new()
            {
                Title = AnnouncementsBusinessMessages.ProcessCompleted,
                Message = AnnouncementsBusinessMessages.SucessCreatedAnnouncementMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
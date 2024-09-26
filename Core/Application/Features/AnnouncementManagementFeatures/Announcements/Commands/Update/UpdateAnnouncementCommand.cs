using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using Core.Enum;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Update;

public class UpdateAnnouncementCommand : IRequest<UpdatedAnnouncementResponse>
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Link { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public ShowType ShowType { get; set; }
    public int RowNo { get; set; }

    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, UpdatedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;

        public UpdateAnnouncementCommandHandler(IMapper mapper, IAnnouncementReadRepository announcementReadRepository, IAnnouncementWriteRepository announcementWriteRepository, AnnouncementBusinessRules announcementBusinessRules)
        {
            _mapper = mapper;
            _announcementReadRepository = announcementReadRepository;
            _announcementWriteRepository = announcementWriteRepository;
            _announcementBusinessRules = announcementBusinessRules;
        }

        public async Task<UpdatedAnnouncementResponse> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(request.Gid.ToString());
            Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: a => a.Gid == request.Gid, cancellationToken: cancellationToken);
            announcement = _mapper.Map(request, announcement);

            _announcementWriteRepository.Update(announcement!);
            await _announcementWriteRepository.SaveAsync();

            GetByGidAnnouncementResponse obj = _mapper.Map<GetByGidAnnouncementResponse>(announcement);

            return new()
            {
                Title = AnnouncementsBusinessMessages.ProcessCompleted,
                Message = AnnouncementsBusinessMessages.SucessUpdateAnnouncementMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
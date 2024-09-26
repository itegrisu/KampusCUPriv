using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;
using Microsoft.VisualBasic;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Delete;

public class DeleteAnnouncementCommand : IRequest<DeletedAnnouncementResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, DeletedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;

        public DeleteAnnouncementCommandHandler(IMapper mapper, IAnnouncementReadRepository announcementReadRepository, IAnnouncementWriteRepository announcementWriteRepository, AnnouncementBusinessRules announcementBusinessRules)
        {
            _mapper = mapper;
            _announcementReadRepository = announcementReadRepository;
            _announcementWriteRepository = announcementWriteRepository;
            _announcementBusinessRules = announcementBusinessRules;
        }

        public async Task<DeletedAnnouncementResponse> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(request.Gid.ToString());
            Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: a => a.Gid == request.Gid, cancellationToken: cancellationToken);

            announcement.DataState = Core.Enum.DataState.Deleted;
            _announcementWriteRepository.Update(announcement);
            await _announcementWriteRepository.SaveAsync();

            return new()
            {
                Title = AnnouncementsBusinessMessages.ProcessCompleted,
                Message = AnnouncementsBusinessMessages.SucessDeletedAnnouncementMessage,
                IsValid = true
            };
        }
    }
}
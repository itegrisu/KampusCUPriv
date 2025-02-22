using Application.Features.CommunicationFeatures.Announcements.Constants;
using Application.Features.CommunicationFeatures.Announcements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Delete;

public class DeleteAnnouncementCommand : IRequest<DeletedAnnouncementResponse>
{
	public Guid Gid { get; set; }

    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand, DeletedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;
        private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
        private readonly IStudentAnnouncementWriteRepository _studentAnnouncementWriteRepository;
        public DeleteAnnouncementCommandHandler(IMapper mapper, IAnnouncementReadRepository announcementReadRepository,
                                         AnnouncementBusinessRules announcementBusinessRules, IAnnouncementWriteRepository announcementWriteRepository, IStudentAnnouncementReadRepository studentAnnouncementReadRepository, IStudentAnnouncementWriteRepository studentAnnouncementWriteRepository)
        {
            _mapper = mapper;
            _announcementReadRepository = announcementReadRepository;
            _announcementBusinessRules = announcementBusinessRules;
            _announcementWriteRepository = announcementWriteRepository;
            _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
            _studentAnnouncementWriteRepository = studentAnnouncementWriteRepository;
        }

        public async Task<DeletedAnnouncementResponse> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            X.Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(announcement);
            announcement.DataState = Core.Enum.DataState.Deleted;

            _announcementWriteRepository.Update(announcement);
            await _announcementWriteRepository.SaveAsync();

            var announcementUser = await _studentAnnouncementReadRepository.GetWhere(sc => sc.GidAnnouncementFK == announcement.Gid).ToListAsync();

            foreach (var item in announcementUser)
            {
                item.DataState = Core.Enum.DataState.Deleted;
                _studentAnnouncementWriteRepository.Update(item);
            }

            await _studentAnnouncementWriteRepository.SaveAsync();

            return new()
            {
                Title = AnnouncementsBusinessMessages.ProcessCompleted,
                Message = AnnouncementsBusinessMessages.SuccessDeletedAnnouncementMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.CommunicationFeatures.StudentAnnouncements.Constants;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Delete;

public class DeleteStudentAnnouncementCommand : IRequest<DeletedStudentAnnouncementResponse>
{
	public Guid Gid { get; set; }

    public class DeleteStudentAnnouncementCommandHandler : IRequestHandler<DeleteStudentAnnouncementCommand, DeletedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
        private readonly IStudentAnnouncementWriteRepository _studentAnnouncementWriteRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public DeleteStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementReadRepository studentAnnouncementReadRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IStudentAnnouncementWriteRepository studentAnnouncementWriteRepository)
        {
            _mapper = mapper;
            _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
            _studentAnnouncementWriteRepository = studentAnnouncementWriteRepository;
        }

        public async Task<DeletedStudentAnnouncementResponse> Handle(DeleteStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            X.StudentAnnouncement? studentAnnouncement = await _studentAnnouncementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);
            studentAnnouncement.DataState = Core.Enum.DataState.Deleted;

            _studentAnnouncementWriteRepository.Update(studentAnnouncement);
            await _studentAnnouncementWriteRepository.SaveAsync();

            return new()
            {
                Title = StudentAnnouncementsBusinessMessages.ProcessCompleted,
                Message = StudentAnnouncementsBusinessMessages.SuccessDeletedStudentAnnouncementMessage,
                IsValid = true
            };
        }
    }
}
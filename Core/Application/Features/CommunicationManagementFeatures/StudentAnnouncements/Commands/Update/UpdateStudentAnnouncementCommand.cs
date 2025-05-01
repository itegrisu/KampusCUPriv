using Application.Features.CommunicationFeatures.StudentAnnouncements.Constants;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;

public class UpdateStudentAnnouncementCommand : IRequest<UpdatedStudentAnnouncementResponse>
{
    public Guid Gid { get; set; }
	public Guid GidUserFK { get; set; }
    public Guid GidAnnouncementFK { get; set; }
    public bool IsRead { get; set; }

    public class UpdateStudentAnnouncementCommandHandler : IRequestHandler<UpdateStudentAnnouncementCommand, UpdatedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementWriteRepository _studentAnnouncementWriteRepository;
        private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public UpdateStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementWriteRepository studentAnnouncementWriteRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IStudentAnnouncementReadRepository studentAnnouncementReadRepository)
        {
            _mapper = mapper;
            _studentAnnouncementWriteRepository = studentAnnouncementWriteRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
            _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
        }

        public async Task<UpdatedStudentAnnouncementResponse> Handle(UpdateStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            X.StudentAnnouncement? studentAnnouncement = await _studentAnnouncementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.AnnouncementFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);
            studentAnnouncement = _mapper.Map(request, studentAnnouncement);

            _studentAnnouncementWriteRepository.Update(studentAnnouncement!);
            await _studentAnnouncementWriteRepository.SaveAsync();
            GetByGidStudentAnnouncementResponse obj = _mapper.Map<GetByGidStudentAnnouncementResponse>(studentAnnouncement);

            return new()
            {
                Title = StudentAnnouncementsBusinessMessages.ProcessCompleted,
                Message = StudentAnnouncementsBusinessMessages.SuccessCreatedStudentAnnouncementMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.CommunicationFeatures.StudentAnnouncements.Constants;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;

public class CreateStudentAnnouncementCommand : IRequest<CreatedStudentAnnouncementResponse>
{
    public Guid GidUserFK { get; set; }
public Guid GidAnnouncementFK { get; set; }

public bool IsRead { get; set; }



    public class CreateStudentAnnouncementCommandHandler : IRequestHandler<CreateStudentAnnouncementCommand, CreatedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementWriteRepository _studentAnnouncementWriteRepository;
        private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public CreateStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementWriteRepository studentAnnouncementWriteRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IStudentAnnouncementReadRepository studentAnnouncementReadRepository)
        {
            _mapper = mapper;
            _studentAnnouncementWriteRepository = studentAnnouncementWriteRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
            _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
        }

        public async Task<CreatedStudentAnnouncementResponse> Handle(CreateStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _studentAnnouncementReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.StudentAnnouncement studentAnnouncement = _mapper.Map<X.StudentAnnouncement>(request);
            //studentAnnouncement.RowNo = maxRowNo + 1;

            await _studentAnnouncementWriteRepository.AddAsync(studentAnnouncement);
            await _studentAnnouncementWriteRepository.SaveAsync();

			X.StudentAnnouncement savedStudentAnnouncement = await _studentAnnouncementReadRepository.GetAsync(predicate: x => x.Gid == studentAnnouncement.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.AnnouncementFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidStudentAnnouncementResponse obj = _mapper.Map<GetByGidStudentAnnouncementResponse>(savedStudentAnnouncement);
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
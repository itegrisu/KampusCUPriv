using Application.Features.CommunicationFeatures.Announcements.Constants;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Announcements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Create;

public class CreateAnnouncementCommand : IRequest<CreatedAnnouncementResponse>
{
    public Guid GidClubFK { get; set; }
    public Guid GidUserFK { get; set; }
    public EnumAnnouncementType? AnnouncementType { get; set; }
    public string Description { get; set; }

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
            //int maxRowNo = await _announcementReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Announcement announcement = _mapper.Map<X.Announcement>(request);
            //announcement.RowNo = maxRowNo + 1;

            await _announcementWriteRepository.AddAsync(announcement);
            await _announcementWriteRepository.SaveAsync();

            X.Announcement savedAnnouncement = await _announcementReadRepository.GetAsync(predicate: x => x.Gid == announcement.Gid, include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidAnnouncementResponse obj = _mapper.Map<GetByGidAnnouncementResponse>(savedAnnouncement);
            return new()
            {
                Title = AnnouncementsBusinessMessages.ProcessCompleted,
                Message = AnnouncementsBusinessMessages.SuccessCreatedAnnouncementMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
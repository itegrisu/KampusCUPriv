using Application.Features.CommunicationFeatures.Announcements.Constants;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Announcements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Domain.Enums;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;
using Application.Helpers;
using Domain.Entities.GeneralManagements;
using AutoMapper.Execution;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Create;

public class CreateAnnouncementCommand : IRequest<CreatedAnnouncementResponse>
{
    public Guid? GidClubFK { get; set; }
    public EnumAnnouncementType? AnnouncementType { get; set; }
    public string Description { get; set; }

    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, CreatedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;
        private readonly IUserReadRepository _userRepository;
        private readonly IStudentClubReadRepository _studentClubRepository;
        private readonly IStudentAnnouncementWriteRepository _studentAnnouncementRepository;
        private readonly IPushNotificationService _pushNotificationService;
        public CreateAnnouncementCommandHandler(IMapper mapper, IAnnouncementWriteRepository announcementWriteRepository,
                                         AnnouncementBusinessRules announcementBusinessRules, IAnnouncementReadRepository announcementReadRepository, IUserReadRepository userRepository, IStudentClubReadRepository studentClubRepository, IStudentAnnouncementWriteRepository studentAnnouncementRepository)
        {
            _mapper = mapper;
            _announcementWriteRepository = announcementWriteRepository;
            _announcementBusinessRules = announcementBusinessRules;
            _announcementReadRepository = announcementReadRepository;
            _userRepository = userRepository;
            _studentClubRepository = studentClubRepository;
            _studentAnnouncementRepository = studentAnnouncementRepository;
        }

        public async Task<CreatedAnnouncementResponse> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            X.Announcement announcement = _mapper.Map<X.Announcement>(request);
            await _announcementWriteRepository.AddAsync(announcement);
            await _announcementWriteRepository.SaveAsync();

            X.Announcement savedAnnouncement = await _announcementReadRepository.GetAsync(predicate: x => x.Gid == announcement.Gid, include: x => x.Include(x => x.ClubFK));

            if (request.AnnouncementType == EnumAnnouncementType.Genel)
            {
                var users = await _userRepository.GetAll().ToListAsync();
                foreach (var user in users)
                {
                    var studentAnnouncement = new CreateStudentAnnouncementCommand
                    {
                        GidUserFK = user.Gid,
                        GidAnnouncementFK = savedAnnouncement.Gid,
                        IsRead = false
                    };
                    await _studentAnnouncementRepository.AddAsync(_mapper.Map<X.StudentAnnouncement>(studentAnnouncement));

                    if (!string.IsNullOrEmpty(user.DeviceToken))
                    {
                        await _pushNotificationService.SendPushNotificationAsync(
                            user.DeviceToken,
                            "Yeni Duyuru",
                            $"Merhaba {user.Name}, yeni bir duyuru var: {savedAnnouncement.Description}"
                        );
                    }
                }
            }
            else if (request.AnnouncementType == EnumAnnouncementType.Kulup && request.GidClubFK.HasValue)
            {
                var clubMembers = await _studentClubRepository.GetListAsync(sc => sc.GidClubFK == request.GidClubFK.Value, include: x => x.Include(x => x.UserFK));
                foreach (var member in clubMembers.Items)
                {
                    var studentAnnouncement = new CreateStudentAnnouncementCommand
                    {
                        GidUserFK = member.GidUserFK,
                        GidAnnouncementFK = savedAnnouncement.Gid,
                        IsRead = false
                    };
                    await _studentAnnouncementRepository.AddAsync(_mapper.Map<X.StudentAnnouncement>(studentAnnouncement));

                    if (!string.IsNullOrEmpty(member.UserFK.DeviceToken))
                    {
                        await _pushNotificationService.SendPushNotificationAsync(
                            member.UserFK.DeviceToken,
                        "Yeni Duyuru",
                            $"Merhaba {member.UserFK.Name}, yeni bir duyuru var: {savedAnnouncement.Description}"
                        );
                    }
                }
            }
            else if (request.AnnouncementType == EnumAnnouncementType.Kan)
            {
                var bloodDonors = await _userRepository.GetWhere(u => u.IsBloodDonor == true).ToListAsync();
                foreach (var donor in bloodDonors)
                {
                    var studentAnnouncement = new CreateStudentAnnouncementCommand
                    {
                        GidUserFK = donor.Gid,
                        GidAnnouncementFK = savedAnnouncement.Gid,
                        IsRead = false
                    };
                    await _studentAnnouncementRepository.AddAsync(_mapper.Map<X.StudentAnnouncement>(studentAnnouncement));

                    if (!string.IsNullOrEmpty(donor.DeviceToken))
                    {
                        await _pushNotificationService.SendPushNotificationAsync(
                            donor.DeviceToken,
                        "Yeni Duyuru",
                            $"Merhaba {donor.Name}, yeni bir duyuru var: {savedAnnouncement.Description}"
                        );
                    }
                }
            }

            await _studentAnnouncementRepository.SaveAsync();


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

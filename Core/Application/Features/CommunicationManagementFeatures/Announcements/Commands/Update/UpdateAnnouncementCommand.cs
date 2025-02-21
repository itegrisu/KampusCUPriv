using Application.Features.CommunicationFeatures.Announcements.Constants;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Announcements.Rules;
using AutoMapper;
using X = Domain.Entities.CommunicationManagements;
using MediatR;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Update;

public class UpdateAnnouncementCommand : IRequest<UpdatedAnnouncementResponse>
{
    public Guid Gid { get; set; }
    public Guid? GidClubFK { get; set; }
    public EnumAnnouncementType? AnnouncementType { get; set; }
    public string Description { get; set; }

    public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, UpdatedAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementWriteRepository _announcementWriteRepository;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly AnnouncementBusinessRules _announcementBusinessRules;

        public UpdateAnnouncementCommandHandler(IMapper mapper, IAnnouncementWriteRepository announcementWriteRepository,
                                         AnnouncementBusinessRules announcementBusinessRules, IAnnouncementReadRepository announcementReadRepository)
        {
            _mapper = mapper;
            _announcementWriteRepository = announcementWriteRepository;
            _announcementBusinessRules = announcementBusinessRules;
            _announcementReadRepository = announcementReadRepository;
        }

        public async Task<UpdatedAnnouncementResponse> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            X.Announcement? announcement = await _announcementReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClubFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _announcementBusinessRules.AnnouncementShouldExistWhenSelected(announcement);
            announcement = _mapper.Map(request, announcement);

            _announcementWriteRepository.Update(announcement!);
            await _announcementWriteRepository.SaveAsync();
            GetByGidAnnouncementResponse obj = _mapper.Map<GetByGidAnnouncementResponse>(announcement);

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
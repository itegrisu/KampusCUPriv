using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Constants;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Rules;
using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.CommunicationManagements;

namespace Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Commands.MarkAllAsRead
{
    public class MarkAllAsReadStudentAnnouncementCommand : IRequest<MarkAllAsReadStudentAnnouncementResponse>
    {
        public List<Guid> StudentAnnouncementGids { get; set; }

        public class MarkAllAsReadStudentAnnouncementCommandHandler : IRequestHandler<MarkAllAsReadStudentAnnouncementCommand, MarkAllAsReadStudentAnnouncementResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStudentAnnouncementWriteRepository _studentAnnouncementWriteRepository;
            private readonly IStudentAnnouncementReadRepository _studentAnnouncementReadRepository;
            private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

            public MarkAllAsReadStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementWriteRepository studentAnnouncementWriteRepository,
                                             StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IStudentAnnouncementReadRepository studentAnnouncementReadRepository)
            {
                _mapper = mapper;
                _studentAnnouncementWriteRepository = studentAnnouncementWriteRepository;
                _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
                _studentAnnouncementReadRepository = studentAnnouncementReadRepository;
            }

            public async Task<MarkAllAsReadStudentAnnouncementResponse> Handle(MarkAllAsReadStudentAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var unreadAnnouncements = await _studentAnnouncementReadRepository.GetListAsync(
                    predicate: x => request.StudentAnnouncementGids.Contains(x.Gid) && !x.IsRead,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(a => a.UserFK).Include(a => a.AnnouncementFK)
                );
                
                if (!unreadAnnouncements.Items.Any())
                {
                    return new()
                    {
                        Title = StudentAnnouncementsBusinessMessages.ProcessCompleted,
                        Message = "No unread announcements found to mark as read.",
                        IsValid = true
                    };
                }

                var readAnnouncements = new List<GetByGidStudentAnnouncementResponse>();

                foreach (var announcement in unreadAnnouncements.Items)
                {
                    announcement.IsRead = true;
                    _studentAnnouncementWriteRepository.Update(announcement);
                    readAnnouncements.Add(_mapper.Map<GetByGidStudentAnnouncementResponse>(announcement));
                }

                await _studentAnnouncementWriteRepository.SaveAsync();

                return new()
                {
                    Title = StudentAnnouncementsBusinessMessages.ProcessCompleted,
                    Message = $"{unreadAnnouncements.Count} announcements marked as read successfully.",
                    IsValid = true,
                    obj = readAnnouncements
                };
            }
        }
    }
}

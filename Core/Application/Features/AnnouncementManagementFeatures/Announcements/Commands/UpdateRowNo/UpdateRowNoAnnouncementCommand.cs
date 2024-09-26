using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Create;
using Application.Features.AnnouncementManagementFeatures.Announcements.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using AutoMapper;
using X = Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UpdateRowNo
{
    public class UpdateRowNoAnnouncementCommand : IRequest<UpdateRowNoAnnouncementResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoAnnouncementCommandHandler : IRequestHandler<UpdateRowNoAnnouncementCommand, UpdateRowNoAnnouncementResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAnnouncementWriteRepository _announcementWriteRepository;
            private readonly IAnnouncementReadRepository _announcementReadRepository;
            private readonly AnnouncementBusinessRules _announcementBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoAnnouncementResponse, X.Announcement> _updateRowNoHelper;

            public UpdateRowNoAnnouncementCommandHandler(IMapper mapper, IAnnouncementWriteRepository announcementWriteRepository,
                                             AnnouncementBusinessRules announcementBusinessRules, IAnnouncementReadRepository announcementReadRepository, UpdateRowNoHelper<UpdateRowNoAnnouncementResponse, X.Announcement> updateRowNoHelper)
            {
                _mapper = mapper;
                _announcementWriteRepository = announcementWriteRepository;
                _announcementBusinessRules = announcementBusinessRules;
                _announcementReadRepository = announcementReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoAnnouncementResponse> Handle(UpdateRowNoAnnouncementCommand request, CancellationToken cancellationToken)
            {
                List<X.Announcement> lst = _announcementReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.Announcement select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoAnnouncementResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _announcementWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
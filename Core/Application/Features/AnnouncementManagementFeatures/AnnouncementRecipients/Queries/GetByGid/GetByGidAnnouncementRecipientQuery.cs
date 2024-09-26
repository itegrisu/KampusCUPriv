using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;

public class GetByGidAnnouncementRecipientQuery : IRequest<GetByGidAnnouncementRecipientResponse>
{
    public Guid Gid { get; set; }

    public class GetByIdAnnouncementRecipientQueryHandler : IRequestHandler<GetByGidAnnouncementRecipientQuery, GetByGidAnnouncementRecipientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
        private readonly AnnouncementRecipientBusinessRules _announcementRecipientBusinessRules;

        public GetByIdAnnouncementRecipientQueryHandler(IMapper mapper, IAnnouncementRecipientReadRepository announcementRecipientReadRepository, AnnouncementRecipientBusinessRules announcementRecipientBusinessRules)
        {
            _mapper = mapper;
            _announcementRecipientReadRepository = announcementRecipientReadRepository;
            _announcementRecipientBusinessRules = announcementRecipientBusinessRules;
        }

        public async Task<GetByGidAnnouncementRecipientResponse> Handle(GetByGidAnnouncementRecipientQuery request, CancellationToken cancellationToken)
        {
            await _announcementRecipientBusinessRules.AnnouncementRecipientShouldExistWhenSelected(request.Gid);

            AnnouncementRecipient? announcementRecipient = await _announcementRecipientReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);

            GetByGidAnnouncementRecipientResponse response = _mapper.Map<GetByGidAnnouncementRecipientResponse>(announcementRecipient);
            return response;
        }
    }
}
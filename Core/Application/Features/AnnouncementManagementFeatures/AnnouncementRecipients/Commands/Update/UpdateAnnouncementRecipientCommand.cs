using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Constants;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Update;

public class UpdateAnnouncementRecipientCommand : IRequest<UpdatedAnnouncementRecipientResponse>
{
    public Guid Gid { get; set; }
    public Guid GidAnnouncementFK { get; set; }
    public Guid GidRecipientFK { get; set; }
    public DateTime? ReadDate { get; set; }
    public string? ReadIpAddress { get; set; }
    public bool? Confirm { get; set; }

    public class UpdateAnnouncementRecipientCommandHandler : IRequestHandler<UpdateAnnouncementRecipientCommand, UpdatedAnnouncementRecipientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
        private readonly IAnnouncementRecipientWriteRepository _announcementRecipientWriteRepository;
        private readonly AnnouncementRecipientBusinessRules _announcementRecipientBusinessRules;

        public UpdateAnnouncementRecipientCommandHandler(IMapper mapper, IAnnouncementRecipientReadRepository announcementRecipientReadRepository, IAnnouncementRecipientWriteRepository announcementRecipientWriteRepository, AnnouncementRecipientBusinessRules announcementRecipientBusinessRules)
        {
            _mapper = mapper;
            _announcementRecipientReadRepository = announcementRecipientReadRepository;
            _announcementRecipientWriteRepository = announcementRecipientWriteRepository;
            _announcementRecipientBusinessRules = announcementRecipientBusinessRules;
        }

        public async Task<UpdatedAnnouncementRecipientResponse> Handle(UpdateAnnouncementRecipientCommand request, CancellationToken cancellationToken)
        {
            await _announcementRecipientBusinessRules.AnnouncementRecipientShouldExistWhenSelected(request.Gid);
            await _announcementRecipientBusinessRules.AnnouncementShouldExistWhenSelected(request.GidAnnouncementFK);
            await _announcementRecipientBusinessRules.RecipientShouldExistWhenSelected(request.GidRecipientFK);

            AnnouncementRecipient? announcementRecipient = await _announcementRecipientReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);
            announcementRecipient = _mapper.Map(request, announcementRecipient);

            _announcementRecipientWriteRepository.Update(announcementRecipient!);
            await _announcementRecipientWriteRepository.SaveAsync();


            GetByGidAnnouncementRecipientResponse obj = _mapper.Map<GetByGidAnnouncementRecipientResponse>(announcementRecipient);

            return new()
            {
                Title = AnnouncementRecipientsBusinessMessages.ProcessCompleted,
                Message = AnnouncementRecipientsBusinessMessages.SuccessUpdatedAnnouncementRecipient,
                IsValid = true
            };
        }
    }
}
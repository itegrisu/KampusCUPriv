using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Constants;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Delete;

public class DeleteAnnouncementRecipientCommand : IRequest<DeletedAnnouncementRecipientResponse>
{
    public Guid Gid { get; set; }

    public class DeleteAnnouncementRecipientCommandHandler : IRequestHandler<DeleteAnnouncementRecipientCommand, DeletedAnnouncementRecipientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
        private readonly IAnnouncementRecipientWriteRepository _announcementRecipientWriteRepository;
        private readonly AnnouncementRecipientBusinessRules _announcementRecipientBusinessRules;

        public DeleteAnnouncementRecipientCommandHandler(IMapper mapper, IAnnouncementRecipientReadRepository announcementRecipientReadRepository, IAnnouncementRecipientWriteRepository announcementRecipientWriteRepository, AnnouncementRecipientBusinessRules announcementRecipientBusinessRules)
        {
            _mapper = mapper;
            _announcementRecipientReadRepository = announcementRecipientReadRepository;
            _announcementRecipientWriteRepository = announcementRecipientWriteRepository;
            _announcementRecipientBusinessRules = announcementRecipientBusinessRules;
        }

        public async Task<DeletedAnnouncementRecipientResponse> Handle(DeleteAnnouncementRecipientCommand request, CancellationToken cancellationToken)
        {
            await _announcementRecipientBusinessRules.AnnouncementRecipientShouldExistWhenSelected(request.Gid);
            AnnouncementRecipient? announcementRecipient = await _announcementRecipientReadRepository.GetAsync(predicate: ar => ar.Gid == request.Gid, cancellationToken: cancellationToken);

            announcementRecipient.DataState = Core.Enum.DataState.Deleted;
            _announcementRecipientWriteRepository.Update(announcementRecipient);
            await _announcementRecipientWriteRepository.SaveAsync();

            return new()
            {
                Title = AnnouncementRecipientsBusinessMessages.ProcessCompleted,
                Message = AnnouncementRecipientsBusinessMessages.SuccessDeletedAnnouncementRecipient,
                IsValid = true
            };
        }
    }
}
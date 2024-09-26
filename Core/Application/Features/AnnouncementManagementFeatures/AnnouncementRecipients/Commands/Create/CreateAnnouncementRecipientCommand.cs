using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Constants;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Rules;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Domain.Entities.AnnouncementManagements;
using MediatR;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Create;

public class CreateAnnouncementRecipientCommand : IRequest<CreatedAnnouncementRecipientResponse>
{
    public Guid GidAnnouncementFK { get; set; }
    public Guid GidRecipientFK { get; set; }
    public Guid GidLevelFK { get; set; }
    public Guid GidResourceFK { get; set; }
    public Guid? TeamGid { get; set; }
    public DateTime? ReadDate { get; set; }
    public string? ReadIpAddress { get; set; }
    public bool? Confirm { get; set; }

    public class CreateAnnouncementRecipientCommandHandler : IRequestHandler<CreateAnnouncementRecipientCommand, CreatedAnnouncementRecipientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRecipientWriteRepository _announcementRecipientWriteRepository;
        private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
        private readonly AnnouncementRecipientBusinessRules _announcementRecipientBusinessRules;
        private readonly IUserReadRepository _userReadRepository;


        public CreateAnnouncementRecipientCommandHandler(IMapper mapper, IAnnouncementRecipientWriteRepository announcementRecipientWriteRepository,
                                         AnnouncementRecipientBusinessRules announcementRecipientBusinessRules, IUserReadRepository userReadRepository, IAnnouncementRecipientReadRepository announcementRecipientReadRepository)
        {
            _mapper = mapper;
            _announcementRecipientWriteRepository = announcementRecipientWriteRepository;
            _announcementRecipientBusinessRules = announcementRecipientBusinessRules;
            _userReadRepository = userReadRepository;
            _announcementRecipientReadRepository = announcementRecipientReadRepository;
        }

        public async Task<CreatedAnnouncementRecipientResponse> Handle(CreateAnnouncementRecipientCommand request, CancellationToken cancellationToken)
        {
            await _announcementRecipientBusinessRules.AnnouncementShouldExistWhenSelected(request.GidAnnouncementFK);
            await _announcementRecipientBusinessRules.RecipientShouldExistWhenSelected(request.GidRecipientFK);

            List<AnnouncementRecipient> announcementRecipients = new();
            int allUserCount = _userReadRepository.Count();

            //anouncement isteðe binaen tekrar yazýlacak


            await _announcementRecipientWriteRepository.AddRangeAsync(announcementRecipients);
            await _announcementRecipientWriteRepository.SaveAsync();

            //AnnouncementRecipient savedAnnouncementRecipient = await _announcementRecipientReadRepository.GetAsync(predicate: x => x.Gid == announcementRecipientForSetUser.Gid,
            //    include: x => x.Include(x => x.UserFK));

            //GetByGidAnnouncementRecipientResponse obj = _mapper.Map<GetByGidAnnouncementRecipientResponse>(savedAnnouncementRecipient);
            return new()
            {
                Title = AnnouncementRecipientsBusinessMessages.ProcessCompleted,
                Message = AnnouncementRecipientsBusinessMessages.SuccessCreatedAnnouncementRecipient,
                IsValid = true,
                Obj = null
            };
        }
    }
}
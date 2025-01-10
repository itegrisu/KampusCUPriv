using Application.Features.DefinitionFeatures.AnnouncementTypes.Constants;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Delete;

public class DeleteAnnouncementTypeCommand : IRequest<DeletedAnnouncementTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteAnnouncementTypeCommandHandler : IRequestHandler<DeleteAnnouncementTypeCommand, DeletedAnnouncementTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementTypeReadRepository _announcementTypeReadRepository;
        private readonly IAnnouncementTypeWriteRepository _announcementTypeWriteRepository;
        private readonly AnnouncementTypeBusinessRules _announcementTypeBusinessRules;

        public DeleteAnnouncementTypeCommandHandler(IMapper mapper, IAnnouncementTypeReadRepository announcementTypeReadRepository,
                                         AnnouncementTypeBusinessRules announcementTypeBusinessRules, IAnnouncementTypeWriteRepository announcementTypeWriteRepository)
        {
            _mapper = mapper;
            _announcementTypeReadRepository = announcementTypeReadRepository;
            _announcementTypeBusinessRules = announcementTypeBusinessRules;
            _announcementTypeWriteRepository = announcementTypeWriteRepository;
        }

        public async Task<DeletedAnnouncementTypeResponse> Handle(DeleteAnnouncementTypeCommand request, CancellationToken cancellationToken)
        {
            X.AnnouncementType? announcementType = await _announcementTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _announcementTypeBusinessRules.AnnouncementTypeShouldExistWhenSelected(announcementType);
            announcementType.DataState = Core.Enum.DataState.Deleted;

            _announcementTypeWriteRepository.Update(announcementType);
            await _announcementTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = AnnouncementTypesBusinessMessages.ProcessCompleted,
                Message = AnnouncementTypesBusinessMessages.SuccessDeletedAnnouncementTypeMessage,
                IsValid = true
            };
        }
    }
}
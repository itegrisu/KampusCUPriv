using Application.Features.DefinitionFeatures.AnnouncementTypes.Constants;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Update;

public class UpdateAnnouncementTypeCommand : IRequest<UpdatedAnnouncementTypeResponse>
{
    public Guid Gid { get; set; }

	
public string Name { get; set; }



    public class UpdateAnnouncementTypeCommandHandler : IRequestHandler<UpdateAnnouncementTypeCommand, UpdatedAnnouncementTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementTypeWriteRepository _announcementTypeWriteRepository;
        private readonly IAnnouncementTypeReadRepository _announcementTypeReadRepository;
        private readonly AnnouncementTypeBusinessRules _announcementTypeBusinessRules;

        public UpdateAnnouncementTypeCommandHandler(IMapper mapper, IAnnouncementTypeWriteRepository announcementTypeWriteRepository,
                                         AnnouncementTypeBusinessRules announcementTypeBusinessRules, IAnnouncementTypeReadRepository announcementTypeReadRepository)
        {
            _mapper = mapper;
            _announcementTypeWriteRepository = announcementTypeWriteRepository;
            _announcementTypeBusinessRules = announcementTypeBusinessRules;
            _announcementTypeReadRepository = announcementTypeReadRepository;
        }

        public async Task<UpdatedAnnouncementTypeResponse> Handle(UpdateAnnouncementTypeCommand request, CancellationToken cancellationToken)
        {
            X.AnnouncementType? announcementType = await _announcementTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _announcementTypeBusinessRules.AnnouncementTypeShouldExistWhenSelected(announcementType);
            announcementType = _mapper.Map(request, announcementType);

            _announcementTypeWriteRepository.Update(announcementType!);
            await _announcementTypeWriteRepository.SaveAsync();
            GetByGidAnnouncementTypeResponse obj = _mapper.Map<GetByGidAnnouncementTypeResponse>(announcementType);

            return new()
            {
                Title = AnnouncementTypesBusinessMessages.ProcessCompleted,
                Message = AnnouncementTypesBusinessMessages.SuccessCreatedAnnouncementTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
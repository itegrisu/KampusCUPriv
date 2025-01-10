using Application.Features.DefinitionFeatures.AnnouncementTypes.Constants;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Rules;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Create;

public class CreateAnnouncementTypeCommand : IRequest<CreatedAnnouncementTypeResponse>
{
    
public string Name { get; set; }



    public class CreateAnnouncementTypeCommandHandler : IRequestHandler<CreateAnnouncementTypeCommand, CreatedAnnouncementTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementTypeWriteRepository _announcementTypeWriteRepository;
        private readonly IAnnouncementTypeReadRepository _announcementTypeReadRepository;
        private readonly AnnouncementTypeBusinessRules _announcementTypeBusinessRules;

        public CreateAnnouncementTypeCommandHandler(IMapper mapper, IAnnouncementTypeWriteRepository announcementTypeWriteRepository,
                                         AnnouncementTypeBusinessRules announcementTypeBusinessRules, IAnnouncementTypeReadRepository announcementTypeReadRepository)
        {
            _mapper = mapper;
            _announcementTypeWriteRepository = announcementTypeWriteRepository;
            _announcementTypeBusinessRules = announcementTypeBusinessRules;
            _announcementTypeReadRepository = announcementTypeReadRepository;
        }

        public async Task<CreatedAnnouncementTypeResponse> Handle(CreateAnnouncementTypeCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _announcementTypeReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.AnnouncementType announcementType = _mapper.Map<X.AnnouncementType>(request);
            //announcementType.RowNo = maxRowNo + 1;

            await _announcementTypeWriteRepository.AddAsync(announcementType);
            await _announcementTypeWriteRepository.SaveAsync();

			X.AnnouncementType savedAnnouncementType = await _announcementTypeReadRepository.GetAsync(predicate: x => x.Gid == announcementType.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidAnnouncementTypeResponse obj = _mapper.Map<GetByGidAnnouncementTypeResponse>(savedAnnouncementType);
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
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Rules;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid
{
    public class GetByGidAnnouncementTypeQuery : IRequest<GetByGidAnnouncementTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidAnnouncementTypeQueryHandler : IRequestHandler<GetByGidAnnouncementTypeQuery, GetByGidAnnouncementTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAnnouncementTypeReadRepository _announcementTypeReadRepository;
            private readonly AnnouncementTypeBusinessRules _announcementTypeBusinessRules;

            public GetByGidAnnouncementTypeQueryHandler(IMapper mapper, IAnnouncementTypeReadRepository announcementTypeReadRepository, AnnouncementTypeBusinessRules announcementTypeBusinessRules)
            {
                _mapper = mapper;
                _announcementTypeReadRepository = announcementTypeReadRepository;
                _announcementTypeBusinessRules = announcementTypeBusinessRules;
            }

            public async Task<GetByGidAnnouncementTypeResponse> Handle(GetByGidAnnouncementTypeQuery request, CancellationToken cancellationToken)
            {
                X.AnnouncementType? announcementType = await _announcementTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _announcementTypeBusinessRules.AnnouncementTypeShouldExistWhenSelected(announcementType);

                GetByGidAnnouncementTypeResponse response = _mapper.Map<GetByGidAnnouncementTypeResponse>(announcementType);
                return response;
            }
        }
    }
}
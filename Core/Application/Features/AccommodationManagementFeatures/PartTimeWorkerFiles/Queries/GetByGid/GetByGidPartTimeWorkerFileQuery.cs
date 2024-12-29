using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Rules;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerFileQuery : IRequest<GetByGidPartTimeWorkerFileResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPartTimeWorkerFileQueryHandler : IRequestHandler<GetByGidPartTimeWorkerFileQuery, GetByGidPartTimeWorkerFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPartTimeWorkerFileReadRepository _partTimeWorkerFileReadRepository;
            private readonly PartTimeWorkerFileBusinessRules _partTimeWorkerFileBusinessRules;

            public GetByGidPartTimeWorkerFileQueryHandler(IMapper mapper, IPartTimeWorkerFileReadRepository partTimeWorkerFileReadRepository, PartTimeWorkerFileBusinessRules partTimeWorkerFileBusinessRules)
            {
                _mapper = mapper;
                _partTimeWorkerFileReadRepository = partTimeWorkerFileReadRepository;
                _partTimeWorkerFileBusinessRules = partTimeWorkerFileBusinessRules;
            }

            public async Task<GetByGidPartTimeWorkerFileResponse> Handle(GetByGidPartTimeWorkerFileQuery request, CancellationToken cancellationToken)
            {
                X.PartTimeWorkerFile? partTimeWorkerFile = await _partTimeWorkerFileReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _partTimeWorkerFileBusinessRules.PartTimeWorkerFileShouldExistWhenSelected(partTimeWorkerFile);

                GetByGidPartTimeWorkerFileResponse response = _mapper.Map<GetByGidPartTimeWorkerFileResponse>(partTimeWorkerFile);
                return response;
            }
        }
    }
}
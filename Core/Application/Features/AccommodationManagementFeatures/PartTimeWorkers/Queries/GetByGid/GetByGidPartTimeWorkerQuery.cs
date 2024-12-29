using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Rules;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerQuery : IRequest<GetByGidPartTimeWorkerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPartTimeWorkerQueryHandler : IRequestHandler<GetByGidPartTimeWorkerQuery, GetByGidPartTimeWorkerResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
            private readonly PartTimeWorkerBusinessRules _partTimeWorkerBusinessRules;

            public GetByGidPartTimeWorkerQueryHandler(IMapper mapper, IPartTimeWorkerReadRepository partTimeWorkerReadRepository, PartTimeWorkerBusinessRules partTimeWorkerBusinessRules)
            {
                _mapper = mapper;
                _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
                _partTimeWorkerBusinessRules = partTimeWorkerBusinessRules;
            }

            public async Task<GetByGidPartTimeWorkerResponse> Handle(GetByGidPartTimeWorkerQuery request, CancellationToken cancellationToken)
            {
                X.PartTimeWorker? partTimeWorker = await _partTimeWorkerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _partTimeWorkerBusinessRules.PartTimeWorkerShouldExistWhenSelected(partTimeWorker);

                GetByGidPartTimeWorkerResponse response = _mapper.Map<GetByGidPartTimeWorkerResponse>(partTimeWorker);
                return response;
            }
        }
    }
}
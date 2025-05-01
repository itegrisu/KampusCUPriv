using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.DefinitionFeatures.Classes.Rules;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;

namespace Application.Features.DefinitionFeatures.Classes.Queries.GetByGid
{
    public class GetByGidClassQuery : IRequest<GetByGidClassResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidClassQueryHandler : IRequestHandler<GetByGidClassQuery, GetByGidClassResponse>
        {
            private readonly IMapper _mapper;
            private readonly IClassReadRepository _classReadRepository;
            private readonly ClassBusinessRules _classBusinessRules;

            public GetByGidClassQueryHandler(IMapper mapper, IClassReadRepository classReadRepository, ClassBusinessRules classBusinessRules)
            {
                _mapper = mapper;
                _classReadRepository = classReadRepository;
                _classBusinessRules = classBusinessRules;
            }

            public async Task<GetByGidClassResponse> Handle(GetByGidClassQuery request, CancellationToken cancellationToken)
            {
                X.Class? class1 = await _classReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _classBusinessRules.ClassShouldExistWhenSelected(class1);

                GetByGidClassResponse response = _mapper.Map<GetByGidClassResponse>(class1);
                return response;
            }
        }
    }
}
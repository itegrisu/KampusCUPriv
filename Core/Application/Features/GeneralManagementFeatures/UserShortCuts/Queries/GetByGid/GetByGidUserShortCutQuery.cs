using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Domain.Entities.GeneralManagements;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid
{
    public class GetByGidUserShortCutQuery : IRequest<GetByGidUserShortCutResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidUserShortCutQueryHandler : IRequestHandler<GetByGidUserShortCutQuery, GetByGidUserShortCutResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserShortCutReadRepository _userShortCutReadRepository;
            private readonly UserShortCutBusinessRules _userShortCutBusinessRules;

            public GetByGidUserShortCutQueryHandler(IMapper mapper, IUserShortCutReadRepository userShortCutReadRepository, UserShortCutBusinessRules userShortCutBusinessRules)
            {
                _mapper = mapper;
                _userShortCutReadRepository = userShortCutReadRepository;
                _userShortCutBusinessRules = userShortCutBusinessRules;
            }

            public async Task<GetByGidUserShortCutResponse> Handle(GetByGidUserShortCutQuery request, CancellationToken cancellationToken)
            {
                UserShortCut? userShortCut = await _userShortCutReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid,
                    include: t => t.Include(t => t.UserFK)
                  , cancellationToken: cancellationToken);
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _userShortCutBusinessRules.UserShortCutShouldExistWhenSelected(userShortCut);

                GetByGidUserShortCutResponse response = _mapper.Map<GetByGidUserShortCutResponse>(userShortCut);
                return response;
            }
        }
    }
}
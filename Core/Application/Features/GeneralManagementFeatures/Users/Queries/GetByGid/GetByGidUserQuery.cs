using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GeneralFeatures.Users.Queries.GetByGid
{
    public class GetByGidUserQuery : IRequest<GetByGidUserResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidUserQueryHandler : IRequestHandler<GetByGidUserQuery, GetByGidUserResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserReadRepository _userReadRepository;
            private readonly UserBusinessRules _userBusinessRules;

            public GetByGidUserQueryHandler(IMapper mapper, IUserReadRepository userReadRepository, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _userReadRepository = userReadRepository;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<GetByGidUserResponse> Handle(GetByGidUserQuery request, CancellationToken cancellationToken)
            {
                X.User? user = await _userReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClassFK).Include(x => x.DepartmentFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _userBusinessRules.UserShouldExistWhenSelected(user);

                GetByGidUserResponse response = _mapper.Map<GetByGidUserResponse>(user);
                return response;
            }
        }
    }
}
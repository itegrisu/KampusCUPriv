using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid
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
                await _userBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);
                User? userCustom = await _userReadRepository.GetAsync(
                    predicate: uc => uc.Gid == request.Gid,
                    cancellationToken: cancellationToken,
                    include: i => i.Include(i => i.CountryFK));

                GetByGidUserResponse response = _mapper.Map<GetByGidUserResponse>(userCustom);
                return response;
            }
        }
    }
}
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid
{
    public class GetByGidPersonnelPassportInfoQuery : IRequest<GetByGidPersonnelPassportInfoResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelPassportInfoQueryHandler : IRequestHandler<GetByGidPersonnelPassportInfoQuery, GetByGidPersonnelPassportInfoResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPassportInfoReadRepository _personnelPassportInfoReadRepository;
            private readonly PersonnelPassportInfoBusinessRules _personnelPassportInfoBusinessRules;

            public GetByGidPersonnelPassportInfoQueryHandler(IMapper mapper, IPersonnelPassportInfoReadRepository personnelPassportInfoReadRepository, PersonnelPassportInfoBusinessRules personnelPassportInfoBusinessRules)
            {
                _mapper = mapper;
                _personnelPassportInfoReadRepository = personnelPassportInfoReadRepository;
                _personnelPassportInfoBusinessRules = personnelPassportInfoBusinessRules;
            }

            public async Task<GetByGidPersonnelPassportInfoResponse> Handle(GetByGidPersonnelPassportInfoQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelPassportInfo? personnelPassportInfo = await _personnelPassportInfoReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));

                await _personnelPassportInfoBusinessRules.PersonnelPassportInfoShouldExistWhenSelected(personnelPassportInfo);

                GetByGidPersonnelPassportInfoResponse response = _mapper.Map<GetByGidPersonnelPassportInfoResponse>(personnelPassportInfo);
                return response;
            }
        }
    }
}
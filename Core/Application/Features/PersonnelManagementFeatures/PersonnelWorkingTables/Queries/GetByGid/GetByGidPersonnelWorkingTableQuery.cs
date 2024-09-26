using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid
{
    public class GetByGidPersonnelWorkingTableQuery : IRequest<GetByGidPersonnelWorkingTableResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPersonnelWorkingTableQueryHandler : IRequestHandler<GetByGidPersonnelWorkingTableQuery, GetByGidPersonnelWorkingTableResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
            private readonly PersonnelWorkingTableBusinessRules _personnelWorkingTableBusinessRules;

            public GetByGidPersonnelWorkingTableQueryHandler(IMapper mapper, IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository, PersonnelWorkingTableBusinessRules personnelWorkingTableBusinessRules)
            {
                _mapper = mapper;
                _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
                _personnelWorkingTableBusinessRules = personnelWorkingTableBusinessRules;
            }

            public async Task<GetByGidPersonnelWorkingTableResponse> Handle(GetByGidPersonnelWorkingTableQuery request, CancellationToken cancellationToken)
            {
                X.PersonnelWorkingTable? personnelWorkingTable = await _personnelWorkingTableReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));

                await _personnelWorkingTableBusinessRules.PersonnelWorkingTableShouldExistWhenSelected(personnelWorkingTable);

                GetByGidPersonnelWorkingTableResponse response = _mapper.Map<GetByGidPersonnelWorkingTableResponse>(personnelWorkingTable);
                return response;
            }
        }
    }
}
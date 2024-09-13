using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Update;

public class UpdatePersonnelWorkingTableCommand : IRequest<UpdatedPersonnelWorkingTableResponse>
{
    public Guid Gid { get; set; }

	public Guid GidPersonelFK { get; set; }

public DateTime IseBaslamaTarihi { get; set; }
public DateTime? IstenCikisTarihi { get; set; }



    public class UpdatePersonnelWorkingTableCommandHandler : IRequestHandler<UpdatePersonnelWorkingTableCommand, UpdatedPersonnelWorkingTableResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelWorkingTableWriteRepository _personnelWorkingTableWriteRepository;
        private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
        private readonly PersonnelWorkingTableBusinessRules _personnelWorkingTableBusinessRules;

        public UpdatePersonnelWorkingTableCommandHandler(IMapper mapper, IPersonnelWorkingTableWriteRepository personnelWorkingTableWriteRepository,
                                         PersonnelWorkingTableBusinessRules personnelWorkingTableBusinessRules, IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository)
        {
            _mapper = mapper;
            _personnelWorkingTableWriteRepository = personnelWorkingTableWriteRepository;
            _personnelWorkingTableBusinessRules = personnelWorkingTableBusinessRules;
            _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
        }

        public async Task<UpdatedPersonnelWorkingTableResponse> Handle(UpdatePersonnelWorkingTableCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelWorkingTable? personnelWorkingTable = await _personnelWorkingTableReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _personnelWorkingTableBusinessRules.PersonnelWorkingTableShouldExistWhenSelected(personnelWorkingTable);
            personnelWorkingTable = _mapper.Map(request, personnelWorkingTable);

            _personnelWorkingTableWriteRepository.Update(personnelWorkingTable!);
            await _personnelWorkingTableWriteRepository.SaveAsync();
            GetByGidPersonnelWorkingTableResponse obj = _mapper.Map<GetByGidPersonnelWorkingTableResponse>(personnelWorkingTable);

            return new()
            {
                Title = PersonnelWorkingTablesBusinessMessages.ProcessCompleted,
                Message = PersonnelWorkingTablesBusinessMessages.SuccessCreatedPersonnelWorkingTableMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Delete;

public class DeletePersonnelWorkingTableCommand : IRequest<DeletedPersonnelWorkingTableResponse>
{
	public Guid Gid { get; set; }

    public class DeletePersonnelWorkingTableCommandHandler : IRequestHandler<DeletePersonnelWorkingTableCommand, DeletedPersonnelWorkingTableResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
        private readonly IPersonnelWorkingTableWriteRepository _personnelWorkingTableWriteRepository;
        private readonly PersonnelWorkingTableBusinessRules _personnelWorkingTableBusinessRules;

        public DeletePersonnelWorkingTableCommandHandler(IMapper mapper, IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository,
                                         PersonnelWorkingTableBusinessRules personnelWorkingTableBusinessRules, IPersonnelWorkingTableWriteRepository personnelWorkingTableWriteRepository)
        {
            _mapper = mapper;
            _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
            _personnelWorkingTableBusinessRules = personnelWorkingTableBusinessRules;
            _personnelWorkingTableWriteRepository = personnelWorkingTableWriteRepository;
        }

        public async Task<DeletedPersonnelWorkingTableResponse> Handle(DeletePersonnelWorkingTableCommand request, CancellationToken cancellationToken)
        {
            X.PersonnelWorkingTable? personnelWorkingTable = await _personnelWorkingTableReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _personnelWorkingTableBusinessRules.PersonnelWorkingTableShouldExistWhenSelected(personnelWorkingTable);
            personnelWorkingTable.DataState = Core.Enum.DataState.Deleted;

            _personnelWorkingTableWriteRepository.Update(personnelWorkingTable);
            await _personnelWorkingTableWriteRepository.SaveAsync();

            return new()
            {
                Title = PersonnelWorkingTablesBusinessMessages.ProcessCompleted,
                Message = PersonnelWorkingTablesBusinessMessages.SuccessDeletedPersonnelWorkingTableMessage,
                IsValid = true
            };
        }
    }
}
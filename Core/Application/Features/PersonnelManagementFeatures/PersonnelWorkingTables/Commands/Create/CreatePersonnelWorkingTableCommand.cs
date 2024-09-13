using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Constants;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using AutoMapper;
using X = Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;

public class CreatePersonnelWorkingTableCommand : IRequest<CreatedPersonnelWorkingTableResponse>
{
    public Guid GidPersonelFK { get; set; }

public DateTime IseBaslamaTarihi { get; set; }
public DateTime? IstenCikisTarihi { get; set; }



    public class CreatePersonnelWorkingTableCommandHandler : IRequestHandler<CreatePersonnelWorkingTableCommand, CreatedPersonnelWorkingTableResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelWorkingTableWriteRepository _personnelWorkingTableWriteRepository;
        private readonly IPersonnelWorkingTableReadRepository _personnelWorkingTableReadRepository;
        private readonly PersonnelWorkingTableBusinessRules _personnelWorkingTableBusinessRules;

        public CreatePersonnelWorkingTableCommandHandler(IMapper mapper, IPersonnelWorkingTableWriteRepository personnelWorkingTableWriteRepository,
                                         PersonnelWorkingTableBusinessRules personnelWorkingTableBusinessRules, IPersonnelWorkingTableReadRepository personnelWorkingTableReadRepository)
        {
            _mapper = mapper;
            _personnelWorkingTableWriteRepository = personnelWorkingTableWriteRepository;
            _personnelWorkingTableBusinessRules = personnelWorkingTableBusinessRules;
            _personnelWorkingTableReadRepository = personnelWorkingTableReadRepository;
        }

        public async Task<CreatedPersonnelWorkingTableResponse> Handle(CreatePersonnelWorkingTableCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _personnelWorkingTableReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.PersonnelWorkingTable personnelWorkingTable = _mapper.Map<X.PersonnelWorkingTable>(request);
            //personnelWorkingTable.RowNo = maxRowNo + 1;

            await _personnelWorkingTableWriteRepository.AddAsync(personnelWorkingTable);
            await _personnelWorkingTableWriteRepository.SaveAsync();

			X.PersonnelWorkingTable savedPersonnelWorkingTable = await _personnelWorkingTableReadRepository.GetAsync(predicate: x => x.Gid == personnelWorkingTable.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidPersonnelWorkingTableResponse obj = _mapper.Map<GetByGidPersonnelWorkingTableResponse>(savedPersonnelWorkingTable);
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
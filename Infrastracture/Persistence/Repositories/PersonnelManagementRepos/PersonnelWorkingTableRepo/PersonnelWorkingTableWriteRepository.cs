using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo
{
    public class PersonnelWorkingTableWriteRepository : WriteRepository<PersonnelWorkingTable>, IPersonnelWorkingTableWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelWorkingTableWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

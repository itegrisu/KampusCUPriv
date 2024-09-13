using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo
{

    public class PersonnelWorkingTableReadRepository : ReadRepository<PersonnelWorkingTable>, IPersonnelWorkingTableReadRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelWorkingTableReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

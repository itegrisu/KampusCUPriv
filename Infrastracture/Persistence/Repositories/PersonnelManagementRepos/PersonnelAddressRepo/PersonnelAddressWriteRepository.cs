using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelAddressRepo
{
    public class PersonnelAddressWriteRepository : WriteRepository<PersonnelAddress>, IPersonnelAddressWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelAddressWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

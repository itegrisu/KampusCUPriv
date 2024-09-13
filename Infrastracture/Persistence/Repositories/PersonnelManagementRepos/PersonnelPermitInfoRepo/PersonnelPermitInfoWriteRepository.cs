using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo
{
    public class PersonnelPermitInfoWriteRepository : WriteRepository<PersonnelPermitInfo>, IPersonnelPermitInfoWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelPermitInfoWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

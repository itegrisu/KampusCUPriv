using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo
{
    public class PersonnelResidenceInfoWriteRepository : WriteRepository<PersonnelResidenceInfo>, IPersonnelResidenceInfoWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelResidenceInfoWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

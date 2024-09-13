using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo
{

    public class PersonnelResidenceInfoReadRepository : ReadRepository<PersonnelResidenceInfo>, IPersonnelResidenceInfoReadRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelResidenceInfoReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

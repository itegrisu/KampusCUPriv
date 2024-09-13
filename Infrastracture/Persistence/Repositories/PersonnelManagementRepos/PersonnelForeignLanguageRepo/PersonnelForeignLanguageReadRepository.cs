using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo
{

    public class PersonnelForeignLanguageReadRepository : ReadRepository<PersonnelForeignLanguage>, IPersonnelForeignLanguageReadRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelForeignLanguageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

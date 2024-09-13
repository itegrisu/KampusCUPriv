using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo
{
    public class PersonnelForeignLanguageWriteRepository : WriteRepository<PersonnelForeignLanguage>, IPersonnelForeignLanguageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelForeignLanguageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

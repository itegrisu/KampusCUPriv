using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo
{
    public class PersonnelPassportInfoWriteRepository : WriteRepository<PersonnelPassportInfo>, IPersonnelPassportInfoWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelPassportInfoWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo
{
    public class PersonnelDocumentWriteRepository : WriteRepository<PersonnelDocument>, IPersonnelDocumentWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelDocumentWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

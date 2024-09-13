using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PersonnelManagements;
using Persistence.Context;

namespace Persistence.Repositories.PersonnelManagementRepos.PersonelGraduatedSchoolRepo
{
    public class PersonnelGraduatedSchoolWriteRepository : WriteRepository<PersonnelGraduatedSchool>, IPersonnelGraduatedSchoolWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PersonnelGraduatedSchoolWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

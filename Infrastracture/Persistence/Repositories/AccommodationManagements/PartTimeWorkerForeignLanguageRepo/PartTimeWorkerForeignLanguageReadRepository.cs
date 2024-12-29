using Application.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.PartTimeWorkerForeignLanguageRepo
{

    public class PartTimeWorkerForeignLanguageReadRepository : ReadRepository<PartTimeWorkerForeignLanguage>, IPartTimeWorkerForeignLanguageReadRepository
    {
        private readonly Emasist2024Context _context;
        public PartTimeWorkerForeignLanguageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

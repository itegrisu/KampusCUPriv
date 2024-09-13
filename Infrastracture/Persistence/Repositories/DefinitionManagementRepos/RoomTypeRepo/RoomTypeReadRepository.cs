using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.RoomTypeRepo
{

    public class RoomTypeReadRepository : ReadRepository<RoomType>, IRoomTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public RoomTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

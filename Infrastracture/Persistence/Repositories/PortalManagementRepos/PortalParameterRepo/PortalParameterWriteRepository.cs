using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PortalManagements;
using Persistence.Context;

namespace Persistence.Repositories.PortalManagementRepos.PortalParameterRepo
{
    public class PortalParameterWriteRepository : WriteRepository<PortalParameter>, IPortalParameterWriteRepository
    {
        private readonly Emasist2024Context _context;
    public PortalParameterWriteRepository(Emasist2024Context context) : base(context)
        {
        _context = context;
    }
}
}

using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PortalManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.PortalManagementRepos.PortalTextRepo
{

    public class PortalTextReadRepository : ReadRepository<PortalText>, IPortalTextReadRepository
    {
        private readonly Emasist2024Context _context;
        public PortalTextReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

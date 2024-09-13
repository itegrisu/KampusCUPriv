using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using Core.Context;
using Core.Repositories.Concretes;
using Domain.Entities.PortalManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.PortalManagementRepos.PortalParameterRepo
{

    public class PortalParameterReadRepository : ReadRepository<PortalParameter>, IPortalParameterReadRepository
    {
        private readonly Emasist2024Context _context;
        public PortalParameterReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

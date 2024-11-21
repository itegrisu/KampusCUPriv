using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagementRepos.DistrictRepo
{
    public class DistrictWriteRepository : WriteRepository<District>, IDistrictWriteRepository
    {
        private readonly Emasist2024Context _context;
        public DistrictWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

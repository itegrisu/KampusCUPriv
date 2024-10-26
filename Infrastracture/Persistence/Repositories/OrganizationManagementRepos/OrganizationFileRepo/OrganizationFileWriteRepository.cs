using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationFileRepo
{
    public class OrganizationFileWriteRepository : WriteRepository<OrganizationFile>, IOrganizationFileWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationFileWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

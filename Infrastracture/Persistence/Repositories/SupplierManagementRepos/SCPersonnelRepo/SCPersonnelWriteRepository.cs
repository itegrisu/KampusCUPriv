using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SupplierManagementRepos.SCPersonnelRepo
{
    public class SCPersonnelWriteRepository : WriteRepository<SCPersonnel>, ISCPersonnelWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCPersonnelWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

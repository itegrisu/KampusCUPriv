using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagementRepos.TyreTypeRepo
{
    public class TyreTypeWriteRepository : WriteRepository<TyreType>, ITyreTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TyreTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

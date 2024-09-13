using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.DocumentTypeRepo
{
    public class DocumentTypeWriteRepository : WriteRepository<DocumentType>, IDocumentTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public DocumentTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

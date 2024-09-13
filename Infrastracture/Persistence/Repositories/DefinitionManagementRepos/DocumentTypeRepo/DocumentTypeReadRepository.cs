using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.DocumentTypeRepo
{

    public class DocumentTypeReadRepository : ReadRepository<DocumentType>, IDocumentTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public DocumentTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}

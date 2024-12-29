using Core.Entities;
using Domain.Entities.DefinitionManagements;

namespace Domain.Entities.AccommodationManagements
{
    public class PartTimeWorkerForeignLanguage : BaseEntity
    {
        public Guid GidPartTimeWorkerFK { get; set; }
        public PartTimeWorker PartTimeWorkerFK { get; set; }
        public Guid GidForeignLanguageFK { get; set; }
        public ForeignLanguage ForeignLanguageFK { get; set; }

    }
}

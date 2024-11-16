using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleDocument : BaseEntity
    {

        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public Guid GidDocumentType { get; set; }
        public DocumentType DocumentTypeFK { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public DateTime DocumentDate { get; set; }
        public DateTime? DocumentLastDate { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}

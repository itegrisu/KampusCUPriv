using Core.Entities;

namespace Domain.Entities.DefinitionManagements
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? Description { get; set; }
    }
}

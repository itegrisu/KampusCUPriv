using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public  Guid Gid { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; }
        //public int CreatedUser { get; set; }
        public virtual DataState DataState { get; set; }
    }
}

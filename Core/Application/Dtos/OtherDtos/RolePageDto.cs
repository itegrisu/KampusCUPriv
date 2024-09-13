using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.OtherDtos
{
    public class RolePageDto
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        [JsonPropertyName("children")]
        public List<AuthRoleDto> Pages { get; set; }
    }
}

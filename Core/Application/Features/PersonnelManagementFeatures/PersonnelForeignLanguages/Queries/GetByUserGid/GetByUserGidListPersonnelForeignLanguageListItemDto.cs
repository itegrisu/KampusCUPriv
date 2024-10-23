using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelForeignLanguageListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidLanguageFK { get; set; }
        public string ForeignLanguageFKName { get; set; }
        public EnumLanguageLevel SpeakingLevel { get; set; }
        public EnumLanguageLevel ReadLevel { get; set; }
    }
}

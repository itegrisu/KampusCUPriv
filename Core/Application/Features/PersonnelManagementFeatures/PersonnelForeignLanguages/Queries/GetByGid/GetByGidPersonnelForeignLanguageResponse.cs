using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Queries.GetByGid
{
    public class GetByGidPersonnelForeignLanguageResponse : IResponse
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
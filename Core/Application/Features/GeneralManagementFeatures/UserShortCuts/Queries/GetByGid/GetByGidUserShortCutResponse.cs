using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid
{
    public class GetByGidUserShortCutResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public int RowNo { get; set; }

    }
}
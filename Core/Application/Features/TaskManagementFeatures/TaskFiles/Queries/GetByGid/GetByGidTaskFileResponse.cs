using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetByGid
{
    public class GetByGidTaskFileResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string TaskFKGid { get; set; }
        //public Task TaskFK { get; set; }
        public string UserFKGid { get; set; }
        //public User UserFK { get; set; }

        public string UserFKFullName { get; set; }
        public string FileTitle { get; set; }
        public string? FileDescription { get; set; }
        public string? UploadedFile { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
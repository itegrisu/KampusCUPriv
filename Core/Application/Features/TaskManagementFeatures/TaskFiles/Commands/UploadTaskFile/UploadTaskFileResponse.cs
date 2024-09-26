using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFile
{
    public class UploadTaskFileResponse : BaseResponse, IResponse
    {
        public string FullPath { get; set; }
    }
}

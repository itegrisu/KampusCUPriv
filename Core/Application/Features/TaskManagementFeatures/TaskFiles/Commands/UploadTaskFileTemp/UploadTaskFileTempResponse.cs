using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFileTemp
{
    public class UploadTaskFileTempResponse : BaseResponse, IResponse
    {
        public string FileName { get; set; }
    }
}
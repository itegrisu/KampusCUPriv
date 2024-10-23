using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UpdatePersonnelSpecialNote
{
    public class UpdatePersonnelSpecialNoteUserResponse : BaseResponse, IResponse
    {
        public GetByGidUserResponse Obj { get; set; }
    }
}

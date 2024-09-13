using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.CreateByCheckBox
{
    public class CreatedByCheckBoxAuthUserRoleResponse : BaseResponse, IResponse
    {
        public GetByGidAuthUserRoleResponse Obj { get; set; }
    }
}

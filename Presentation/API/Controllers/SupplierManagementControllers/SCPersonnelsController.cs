using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetList;
using Application.Features.SupplierManagementFeatures.SCEmployers.Queries.GetByCompanyGid;
using Application.Features.SupplierManagementFeatures.SCPersonnels.Queries.GetByCompanyGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCPersonnelsController : BaseController<CreateSCPersonnelCommand, DeleteSCPersonnelCommand, UpdateSCPersonnelCommand, GetByGidSCPersonnelQuery,
          CreatedSCPersonnelResponse, DeletedSCPersonnelResponse, UpdatedSCPersonnelResponse, GetByGidSCPersonnelResponse>
    {
        public SCPersonnelsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSCPersonnelQuery getListSCPersonnelQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSCPersonnelListItemDto> response = await Mediator.Send(getListSCPersonnelQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCompanyGid([FromQuery] GetByCompanyGidListSCPersonnelQuery getByCompanyGidListSCPersonnelQuery)
        {
            GetListResponse<GetByCompanyGidListSCPersonnelListItemDto> response = await Mediator.Send(getByCompanyGidListSCPersonnelQuery);
            return Ok(response);
        }
    }
}

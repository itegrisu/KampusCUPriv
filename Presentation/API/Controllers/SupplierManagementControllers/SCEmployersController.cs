using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCEmployersController : BaseController<CreateSCEmployerCommand, DeleteSCEmployerCommand, UpdateSCEmployerCommand, GetByGidSCEmployerQuery,
         CreatedSCEmployerResponse, DeletedSCEmployerResponse, UpdatedSCEmployerResponse, GetByGidSCEmployerResponse>
    {
        public SCEmployersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSCEmployerQuery getListSCEmployerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSCEmployerListItemDto> response = await Mediator.Send(getListSCEmployerQuery);
            return Ok(response);
        }


    }
}

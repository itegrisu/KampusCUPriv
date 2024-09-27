using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCAddressesController : BaseController<CreateSCAddressCommand, DeleteSCAddressCommand, UpdateSCAddressCommand, GetByGidSCAddressQuery,
       CreatedSCAddressResponse, DeletedSCAddressResponse, UpdatedSCAddressResponse, GetByGidSCAddressResponse>
    {
        public SCAddressesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSCAddressQuery getListSCAddressQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSCAddressListItemDto> response = await Mediator.Send(getListSCAddressQuery);
            return Ok(response);
        }


    }
}

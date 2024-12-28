using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupplierManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SCCompaniesController : BaseController<CreateSCCompanyCommand, DeleteSCCompanyCommand, UpdateSCCompanyCommand, GetByGidSCCompanyQuery,
        CreatedSCCompanyResponse, DeletedSCCompanyResponse, UpdatedSCCompanyResponse, GetByGidSCCompanyResponse>
    {
        public SCCompaniesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListSCCompanyQuery getListSCCompanyQuery)
        {
            GetListResponse<GetListSCCompanyListItemDto> response = await Mediator.Send(getListSCCompanyQuery);
            return Ok(response);
        }


    }
}

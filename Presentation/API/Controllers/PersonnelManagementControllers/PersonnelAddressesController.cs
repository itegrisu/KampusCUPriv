using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PersonnelManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelAddressesController : BaseController<CreatePersonnelAddressCommand, DeletePersonnelAddressCommand, UpdatePersonnelAddressCommand, GetByGidPersonnelAddressQuery,
       CreatedPersonnelAddressResponse, DeletedPersonnelAddressResponse, UpdatedPersonnelAddressResponse, GetByGidPersonnelAddressResponse>
    {
        public PersonnelAddressesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPersonnelAddressQuery getListPersonnelAddressQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPersonnelAddressListItemDto> response = await Mediator.Send(getListPersonnelAddressQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListPersonnelAddressQuery getByUserGidListPersonnelAddressQuery)
        {
            GetListResponse<GetByUserGidListPersonnelAddressListItemDto> response = await Mediator.Send(getByUserGidListPersonnelAddressQuery);
            return Ok(response);
        }


    }
}

using Application.Features.ClubFeatures.Clubs.Commands.Create;
using Application.Features.ClubFeatures.Clubs.Commands.Delete;
using Application.Features.ClubFeatures.Clubs.Commands.Update;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Application.Features.ClubFeatures.Clubs.Queries.GetList;
using Application.Features.ClubManagementFeatures.Clubs.Queries.GetByCount;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByCount;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClubManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : BaseController<CreateClubCommand, DeleteClubCommand, UpdateClubCommand, GetByGidClubQuery,
        CreatedClubResponse, DeletedClubResponse, UpdatedClubResponse, GetByGidClubResponse>
    {
        public ClubsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListClubQuery getListClubQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListClubListItemDto> response = await Mediator.Send(getListClubQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCount([FromQuery] GetByCountListClubQuery getByCountListClubQuery)
        {
            GetListResponse<GetByCountListClubListItemDto> response = await Mediator.Send(getByCountListClubQuery);
            return Ok(response);
        }

    }
}

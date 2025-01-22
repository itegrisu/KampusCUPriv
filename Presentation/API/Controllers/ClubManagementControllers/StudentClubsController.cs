using Application.Features.ClubFeatures.StudentClubs.Commands.Create;
using Application.Features.ClubFeatures.StudentClubs.Commands.Delete;
using Application.Features.ClubFeatures.StudentClubs.Commands.Update;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClubManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentClubsController : BaseController<CreateStudentClubCommand, DeleteStudentClubCommand, UpdateStudentClubCommand, GetByGidStudentClubQuery,
          CreatedStudentClubResponse, DeletedStudentClubResponse, UpdatedStudentClubResponse, GetByGidStudentClubResponse>
    {
        public StudentClubsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListStudentClubQuery getListStudentClubQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListStudentClubListItemDto> response = await Mediator.Send(getListStudentClubQuery);
            return Ok(response);
        }
    }
}

using API.Filters;
using Application.Features.Base;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Admin")] 
public class BaseController<TCreateCommand, TDeleteCommand, TUpdateCommand,
                            TGetByGidQuery,
                            TCreateResponse, TDeleteResponse, TUpdateResponse,
                            TGetByGidResponse
    > : ControllerBase
    where TCreateCommand : IRequest<TCreateResponse>
    where TDeleteCommand : IRequest<TDeleteResponse>
    where TGetByGidQuery : IRequest<TGetByGidResponse>
    where TCreateResponse : BaseResponse
    where TUpdateCommand : IRequest<TUpdateResponse>
    where TDeleteResponse : BaseResponse
    where TUpdateResponse : BaseResponse
    where TGetByGidResponse : IResponse
{
    protected readonly IMediator Mediator;
    protected readonly clsAuth _clsAuth;

    public BaseController(IMediator mediator, clsAuth clsAuth)
    {
        Mediator = mediator;
        _clsAuth = clsAuth;
    }

    [HttpPost("[action]")]
    
    public virtual async Task<IActionResult> Add([FromBody] TCreateCommand request)
    {
        TCreateResponse response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("[action]")]
    
    public virtual async Task<IActionResult> Update([FromBody] TUpdateCommand request)
    {
        TUpdateResponse response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("[action]/{Gid}")]
    
    public async Task<IActionResult> Delete([FromRoute] TDeleteCommand deleteCommand)
    {
        TDeleteResponse response = await Mediator.Send(deleteCommand);
        return Ok(response);
    }

    [HttpGet("[action]")]
    
    public async Task<IActionResult> GetByGid([FromQuery] TGetByGidQuery getByIdQuery)
    {
        TGetByGidResponse response = await Mediator.Send(getByIdQuery);
        return Ok(response);
    }
}
using eAppointmentServer.Application.Features.Roles.RoleSync;
using eAppointmentServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebApi.Controllers;

[AllowAnonymous]
public sealed class RolesController(IMediator mediator) : ApiController(mediator)
{

    [HttpPost]
    public async Task<IActionResult> RoleSync(RoleSyncCommand request,CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
}

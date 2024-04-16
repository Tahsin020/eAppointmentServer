using eAppointmentServer.Application.Features.Patients.CreatePatient;
using eAppointmentServer.Application.Features.Patients.DeletePatientById;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
using eAppointmentServer.Application.Features.Patients.UpdatePatient;
using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.DeleteUserById;
using eAppointmentServer.Application.Features.Users.GetAllRolesForUsers;
using eAppointmentServer.Application.Features.Users.GetAllUsers;
using eAppointmentServer.Application.Features.Users.UpdateUser;
using eAppointmentServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebApi.Controllers
{
    public sealed class UsersController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRoles(GetAllRolesForUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}

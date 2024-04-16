using eAppointmentServer.Application.Features.Patients.CreatePatient;
using eAppointmentServer.Application.Features.Patients.DeletePatientById;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
using eAppointmentServer.Application.Features.Patients.UpdatePatient;
using eAppointmentServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebApi.Controllers
{
    public class PatientsController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}

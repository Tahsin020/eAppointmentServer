using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IPatientRepository patientRepository) : IRequestHandler<CreateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        Patient patient = new();
        if (request.PatientId is null)
        {
            patient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                City = request.City,
                FullAddress = request.FullAddress,
                Town = request.Town
            };
            await patientRepository.AddAsync(patient, cancellationToken);
        }

        DateTime startDate = Convert.ToDateTime(request.StartDate);
        DateTime endDate = Convert.ToDateTime(request.EndDate);

        bool isAppointmentDateNotAvailable = await appointmentRepository.AnyAsync(
             p => p.DoctorId == request.DoctorId &&
             ((p.StartDate < endDate && p.StartDate >= startDate) ||
             (p.EndDate > startDate && p.EndDate <= endDate) ||
             (p.StartDate >= startDate && p.EndDate <= endDate) ||
             (p.StartDate <= startDate && p.EndDate >= endDate)), cancellationToken);

        if (isAppointmentDateNotAvailable)
        {
            return Result<string>.Failure("Appointment date is not available.");
        }

        Appointment appointment = new()
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId ?? patient.Id,
            StartDate = startDate,
            EndDate = endDate,
            IsCompleted = false
        };
        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Appointment create is successful";
    }
}

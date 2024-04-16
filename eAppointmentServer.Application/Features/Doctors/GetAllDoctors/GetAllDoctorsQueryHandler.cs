using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctors;

public sealed class GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        List<Doctor> doctors = await doctorRepository
            .GetAll()
            .OrderBy(P => P.Department)
            .ThenBy(P => P.FirstName)
            .ToListAsync(cancellationToken);
        return doctors;
    }
}


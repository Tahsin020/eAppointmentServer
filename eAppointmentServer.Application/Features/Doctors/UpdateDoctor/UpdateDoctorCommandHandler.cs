using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor;

internal sealed class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionWithTrackingAsync(d => d.Id == request.Id,cancellationToken);

        if (doctor is null)
        {
            return Result<string>.Failure("Doctor not found");
        }
        mapper.Map(request, doctor);

        doctorRepository.Update(doctor);
        await unitOfWork.SaveChangesAsync();
        return "Doctor update is successful";
    }
}


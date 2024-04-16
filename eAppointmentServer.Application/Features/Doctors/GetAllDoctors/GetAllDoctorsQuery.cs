using eAppointmentServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.GetAllDoctors;

public sealed class GetAllDoctorsQuery() : IRequest<Result<List<Doctor>>>;


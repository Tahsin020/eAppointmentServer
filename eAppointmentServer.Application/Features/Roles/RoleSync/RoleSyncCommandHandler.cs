using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Roles.RoleSync;

internal sealed class RoleSyncCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<RoleSyncCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
    {
        List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

        List<AppRole> staticRoles = Constants.GetRoles();

        foreach (AppRole role in currentRoles)
        {
            if (!staticRoles.Any(x => x.Name == role.Name))
            {
                await roleManager.DeleteAsync(role);
            }
        }

        foreach (AppRole role in staticRoles)
        {
            if (!currentRoles.Any(x => x.Name == role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }

        return "Sync is successful";
    }
}


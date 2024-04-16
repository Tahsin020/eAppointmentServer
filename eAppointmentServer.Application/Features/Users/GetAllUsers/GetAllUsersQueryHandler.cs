using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(UserManager<AppUser> userManager, IUserRoleRepository userRoleRepository, RoleManager<AppRole> roleManager) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
{
    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> users = await userManager.Users.OrderBy(p => p.FirstName).ToListAsync(cancellationToken);

        List<GetAllUsersQueryResponse> response = users.Select(s => new GetAllUsersQueryResponse()
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            FullName = s.FullName,
            Email = s.Email,
            UserName = s.UserName
        }).ToList();

        foreach (var item in response)
        {
            List<AppUserRole> userRoles = await userRoleRepository.Where(p => p.UserId == item.Id).ToListAsync(cancellationToken);


            List<Guid> stringRoles = new();
            List<string?> stringRoleNames = new();

            foreach (var userRole in userRoles)
            {
                AppRole? role =
                    await roleManager.Roles
                    .Where(x => x.Id == userRole.RoleId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (role is not null)
                {
                   stringRoleNames.Add(role.Name);
                   stringRoles.Add(role.Id);
                }
            }
            item.RoleIds = stringRoles;
            item.RoleNames = stringRoleNames;
            item.RoleNames = stringRoleNames;
        }
        return response;
    }
}


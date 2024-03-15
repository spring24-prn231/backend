using AutoMapper;
using BusinessObjects.Common.Constants;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Services.Implements
{
    public class InitializeAdminAccountService : IHostedService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole<Guid>> _roleManager;
        private IConfiguration _configuration;
        private IMapper _mapper;
        public IServiceScopeFactory _serviceScopeFactory;
        public InitializeAdminAccountService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            if (!await _roleManager.RoleExistsAsync(UserRole.ADMIN.ToString())) await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.ADMIN.ToString()));
            if (!await _roleManager.RoleExistsAsync(UserRole.USER.ToString())) await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.USER.ToString()));
            if (!await _roleManager.RoleExistsAsync(UserRole.HOST_STAFF.ToString())) await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.HOST_STAFF.ToString()));
            if (!await _roleManager.RoleExistsAsync(UserRole.IMPLEMENT_STAFF.ToString())) await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.IMPLEMENT_STAFF.ToString()));

            var newAdmins = _configuration.GetSection("AdminAccounts").Get<List<ApplicationUser>>();
            if (!newAdmins.IsNullOrEmpty())
            {
                foreach (var admin in newAdmins)
                {
                    var existAdmin = await _userManager.FindByEmailAsync(admin.Email);
                    if (existAdmin == null)
                    {
                        await _userManager.CreateAsync(admin, admin.PasswordHash);
                        await _userManager.AddToRoleAsync(admin, UserRole.ADMIN.ToString());
                    }
                    else if(await _userManager.IsInRoleAsync(existAdmin, UserRole.ADMIN.ToString()))
                    {
                        _mapper.Map(admin, existAdmin);
                        await _userManager.UpdateAsync(existAdmin);
                        var token = await _userManager.GeneratePasswordResetTokenAsync(existAdmin);
                        var result = await _userManager.ResetPasswordAsync(existAdmin, token, admin.PasswordHash);
                    }
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

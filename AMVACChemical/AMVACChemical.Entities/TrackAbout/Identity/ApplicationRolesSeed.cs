using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMVACChemical.Entities.TrackAbout.Identity
{
    public class ApplicationRolesSeed
    {
        private readonly AMVAC_IdentityContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public ApplicationRolesSeed(AMVAC_IdentityContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // Used for seeding the roles
        public async Task Seed(IHostingEnvironment env, ILogger logger)
        {
            if ((await _roleManager.FindByNameAsync("Member")) == null)
            {
               await _roleManager.CreateAsync(new ApplicationRole { Name = "Member" });
            }

            if ((await _roleManager.FindByNameAsync("Admin")) == null)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
            }

            if ((await _roleManager.FindByNameAsync("User")) == null)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = "User" });
            }
        }
    }
}

using AMVACChemical.Entities.TrackAbout.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMVACChemical.Entities
{
    public class AMVAC_IdentityContext:IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        // create constructor 
        public AMVAC_IdentityContext(DbContextOptions<AMVAC_IdentityContext> options) : base(options)
        {
        }
    }
}

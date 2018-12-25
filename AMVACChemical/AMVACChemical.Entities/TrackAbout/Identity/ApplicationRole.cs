using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.Entities.TrackAbout.Identity
{
    public class ApplicationRole:IdentityRole<string>
    {
        public ApplicationRole() : base()
        {

            Id = Guid.NewGuid().ToString();
        }
    }
}

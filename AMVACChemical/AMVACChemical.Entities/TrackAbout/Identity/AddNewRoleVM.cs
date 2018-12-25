using System;
using System.Collections.Generic;
using System.Text;

namespace AMVACChemical.Entities.TrackAbout.Identity
{
    public class AddNewRoleVM
    {
        public ApplicationUser User { get; set; }

        public string NewRole { get; set; }
    }
}

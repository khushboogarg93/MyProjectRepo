using System;
using System.ComponentModel.DataAnnotations;

namespace AMVACChemical.Entities.TrackAbout.Assets
{
    public class Login
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}

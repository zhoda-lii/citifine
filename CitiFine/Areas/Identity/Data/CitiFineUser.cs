using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CitiFine.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CitiFineUser class
public class CitiFineUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LicensePlate { get; set; }
}


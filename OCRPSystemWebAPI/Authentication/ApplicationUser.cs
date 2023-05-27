using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCRPSystemWebAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; internal set; }
        public string Password { get; internal set; }
    }
}

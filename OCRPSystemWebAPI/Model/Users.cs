using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OCRPSystemWebAPI.Model
{
    public partial class Users
    {
        public Users()
        {
            //Recipe = new HashSet<Recipe>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string EmailId { get; set; }

        //public virtual ICollection<Recipe> Recipe { get; set; }
    }
}

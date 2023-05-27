using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OCRPSystemWebAPI.Model
{
    public partial class Category
    {
        public Category()
        {
            Recipe = new HashSet<Recipe>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}

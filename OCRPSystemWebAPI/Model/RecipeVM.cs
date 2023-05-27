using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCRPSystemWebAPI.Model
{
    public class RecipeVM
    {
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string RecipeSteps { get; set; }
        public int? Serves { get; set; }
        public string RecipeStatus { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string EmailId { get; set; }

        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}

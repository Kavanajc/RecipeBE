using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OCRPSystemWebAPI.Model
{
    public partial class Recipe
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
        public int UserId { get; set; }
        public int StateId { get; set; }

        public virtual Category Category { get; set; }
        public virtual State State { get; set; }
        public virtual Users User { get; set; }
    }
}

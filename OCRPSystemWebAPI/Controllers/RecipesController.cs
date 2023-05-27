using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Nest;
using NETCore.MailKit.Core;
using OCRPSystemWebAPI.Model;

namespace OCRPSystemWebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly OCRP_SystemContext _context;
        //private readonly IEmailService _emailService;
        public RecipesController(OCRP_SystemContext context) //IEmailService emailService
        {
            _context = context;
            //_emailService = emailService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeVM>>> GetRecipe()
        {
            //return await _context.Recipe.ToListAsync();
            var query = from Recipe in _context.Recipe
                        join state in _context.State on Recipe.StateId equals state.StateId
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join Users in _context.Users on Recipe.UserId equals Users.UserId
                        where Recipe.RecipeStatus=="Approved"
                        select new
                        {
                            Recipe_Id = Recipe.RecipeId,
                            Description = Recipe.Description,
                            Title = Recipe.Title,
                            Ingredients = Recipe.Ingredients,
                            Recipe_steps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            Recipe_status = Recipe.RecipeStatus,
                            Image_Url = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = state.StateName,
                            UserName = Users.UserName
                        };
            return Ok(query);

        }


        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeVM>> GetRecipe(int id)
        {
            var query = from Recipe in _context.Recipe
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join Users in _context.Users on Recipe.UserId equals Users.UserId
                        where Recipe.RecipeId.Equals(id)
                        select new
                        {
                            Recipe_Id = Recipe.RecipeId,
                            Description = Recipe.Description,
                            Title = Recipe.Title,
                            Ingredients = Recipe.Ingredients,
                            Recipe_steps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            Recipe_status = Recipe.RecipeStatus,
                            Image_Url = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = Users.UserName
                        };
            return Ok(query);


        }






        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
          try
            {
                if (recipe != null)
                {
                    Recipe obj = _context.Recipe.FirstOrDefault(e => e.RecipeId == id);
                    obj.Description = recipe.Description;
                    obj.Title = recipe.Title;
                    obj.Ingredients = recipe.Ingredients;
                    obj.RecipeSteps = recipe.RecipeSteps;
                    obj.Serves = recipe.Serves;
                    obj.RecipeStatus = "Pending";
                    obj.ImageUrl = recipe.ImageUrl;
                    obj.CategoryId = recipe.CategoryId;
                    obj.StateId = recipe.StateId;
                    obj.UserId = recipe.UserId;




                    _context.Recipe.Update(obj);
                    ////Search Category Object
                    //Category catObj = _context.Category.FirstOrDefault(e => e.CategoryId == recipe.CategoryId);
                    //catObj.CategoryName = recipe.Category.CategoryName;




                    //_context.Category.Update(catObj);
                    //await _context.SaveChangesAsync();
                    ////Search State Object
                    //State stateObj = _context.State.FirstOrDefault(e => e.StateId == recipe.StateId);
                    //stateObj.StateName = recipe.State.StateName;




                    //_context.State.Update(stateObj);
                    //await _context.SaveChangesAsync();
                    ////Search Users Object
                    //Users usersObj = _context.Users.FirstOrDefault(e => e.UserId == recipe.UserId);
                    //usersObj.UserName = recipe.User.UserName;



                    //_context.Users.Update(usersObj);
                    ////Reflecting changes to the server
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



               // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }


        //SearchByState
        [HttpGet("search-By_State")]
        public IActionResult SearchRecipeByState(string StateName)
        {
            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where State.StateName.Contains(StateName) && Recipe.RecipeStatus == "Approved"
                        select new
                        {
                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };

            if (!query.Any())
            {
                return NotFound();
            }
            return Ok(query);
        }


        //SearchByVeg
        [HttpGet("search-Recipe-By_Veg")]
        public IActionResult SearchRecipeByCategoryVeg(string CategoryName)
        {
            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where Category.CategoryName == "VEG" && Recipe.RecipeStatus == "Approved"
                        select new
                        {
                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };

            if (!query.Any())
            {
                return NotFound();
            }
            return Ok(query);
        }

        //SearchByNONVeg
        [HttpGet("search-Recipe-By_NonVeg")]
        public IActionResult SearchRecipeByCategoryNonVeg(string CategoryName)
        {
            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where Category.CategoryName == "NON VEG" && Recipe.RecipeStatus == "Approved"
                        select new
                        {
                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };

            if (!query.Any())
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpGet("search-Recipe-by-User")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipeByName(string user)
        {


            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where User.UserName == user
                        select new
                        {


                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };

            if (!query.Any())
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpGet("Pending")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchPendingRecipe()
        {


            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where Recipe.RecipeStatus == "Pending"
                        select new
                        {


                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                            RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };

            if (!query.Any())
            {
                return NotFound();
            }
            
            return Ok(query);
        }

        [HttpPatch("search-Recipe-by-Status")]
        public async Task<ActionResult<IEnumerable<Recipe>>> SearchRecipeByStatus(int id,Recipe recipe)
        {


            var query = from Recipe in _context.Recipe
                        join Category in _context.Category on Recipe.CategoryId equals Category.CategoryId
                        join State in _context.State on Recipe.StateId equals State.StateId
                        join User in _context.Users on Recipe.UserId equals User.UserId
                        where Recipe.RecipeStatus == "Pending"
                        select new
                        {


                            RecipeId = Recipe.RecipeId,
                            Title = Recipe.Title,
                            Description = Recipe.Description,
                            Ingredients = Recipe.Ingredients,
                            RecipeSteps = Recipe.RecipeSteps,
                            Serves = Recipe.Serves,
                           //RecipeStatus = Recipe.RecipeStatus,
                            ImageUrl = Recipe.ImageUrl,
                            CategoryName = Category.CategoryName,
                            StateName = State.StateName,
                            UserName = User.UserName

                        };
            Recipe obj = _context.Recipe.FirstOrDefault(e => e.RecipeId == id);
            obj.RecipeStatus = recipe.RecipeStatus;
            _context.Recipe.Update(obj);

            if (!query.Any())
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Ok(query);
        }


        [HttpPost("Add-Recipe")]
        public async Task<ActionResult<Recipe>> PostCategory(Recipe recipe)
        {
            _context.Recipe.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.RecipeId }, recipe);
        }

        
        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.RecipeId == id);
        }
    }
}

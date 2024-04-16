using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Diagnostics.Contracts;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase

    {

        private readonly IProduct productRepo;

        public CategoriesController(IProduct productRepo)

        {

            this.productRepo = productRepo;

        }

        // GET: api/Categories

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()

        {

            var result =  productRepo.GetAllCategories();

            if (result != null)

            {

                return Ok(result);

            }

            return NoContent();

        }

        // GET: api/Categories/5

        [HttpGet("{id}")]

        public async Task<ActionResult<Category>> GetCategory(int id)

        {

            var category = productRepo.GetCategoriesByCategoryId(id);

            if (category == null)

            {

                return NotFound();

            }

            return category;

        }

        // PUT: api/Categories/5

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutCategory(int id, [FromBody] Category category)

        {

            if (id != category.CategoryId)

            {

                return BadRequest();

            }



            try

            {

                 productRepo.UpdateCategory(id, category);

            }

            catch (DbUpdateConcurrencyException)

            {

                if (!CategoryExists(id))

                {

                    return NotFound();

                }

                else

                {

                    throw;

                }

            }

            return NoContent();

        }


        [HttpPost]

        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)

        {

            await productRepo.AddCategory(category);

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

        }

        // DELETE: api/Categories/5

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCategory(int id)

        {

            var category = await productRepo.DeleteCategory(id);

            if (category == null)

            {

                return NoContent();

            }


            return Ok("Deleted successfully");

        }

        private bool CategoryExists(int id)

        {

            return productRepo.CategoryExists(id);

        }

    }
}


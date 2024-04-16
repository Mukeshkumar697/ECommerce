using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Controllers
{
 
    [Route("api/[controller]")]

    [ApiController]

    public class ProductsController : ControllerBase

    {

        private readonly IProduct productRepo;

        public ProductsController(IProduct productRepo)

        {

            this.productRepo = productRepo;

        }

        // GET: api/Products

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()

        {

            var result = productRepo.GetAllProducts();

            return Ok(result);

        }

        // GET: api/Products/5

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProductById(int id)

        {

            var product = productRepo.GetProductById(id);

            if (product == null)

            {

                return Ok("No product found in this product id");

            }

            return product;

        }

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateProduct(int id, Product product)

        {

            try

            {

                productRepo.UpdateProduct(id, product);

            }

            catch (DbUpdateConcurrencyException)

            {

                if (!ProductExists(id))

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

        public IActionResult AddProduct(Product product)
        {
            var result = productRepo.AddProduct(product);

            if (result == false )
            {
                return Ok("Category id is mismatched. Without category id can not add product.");
            }
            

            return Ok(result);

        }

        // DELETE: api/Products/5

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public IActionResult DeleteProduct(int id)

        {

            var product =  productRepo.DeleteProduct(id);

            if (product == null)

            {

                return NotFound();

            }

            return Ok(product);

        }

        private bool ProductExists(int id)

        {

            return productRepo.ProductExists(id);

        }

        [HttpGet]

        [Route("search/{searchItem}")]

        public async Task<ActionResult<IEnumerable<Product>>> SearchProductByNameOrDesc(string searchItem)

        {

            var result =  productRepo.SearchProduct(searchItem);

            if (result != null)

            {

                return Ok(result);

            }

            return NoContent();

        }

        [HttpGet("GetAllProductsByCategoryId")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)

        {

            var result =  productRepo.GetProductsByCategoryId(categoryId);

            if (result == null)

            {

                return NotFound();

            }

            return Ok(result);

        }


    }

}



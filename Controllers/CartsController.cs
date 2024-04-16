using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase

    {

        private readonly ICart cartRepo;

        public CartsController(ICart cartRepo)

        {

            this.cartRepo = cartRepo;

        }

        // GET: api/Carts

        [HttpGet]

        [Route("GetCartItems")]

       // [Authorize(Roles = "Customer")]

        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()

        {

            var result = cartRepo.GetAllCartItems(GetIdFromToken());

            return Ok(result);

        }
  

        [HttpPost]

        [Route("AddItemToCart")]

        // [Authorize(Roles = "Customer")]

        public async Task<ActionResult<Cart>> PostCart(int productId)

        {

            var result = await cartRepo.AddToCart(productId, GetIdFromToken());

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Product not exist");
            }

        }

        // DELETE: api/Carts/5

        [HttpDelete]

        [Route("RemoveItem")]

        //[Authorize(Roles = "Customer")]

        public IActionResult RemoveItem(int productId)

        {

            var cart = cartRepo.RemoveItem(productId, GetIdFromToken());

            if (cart == false)

            {

                return NotFound();

            }

            return Ok("Item removed from Cart");

        }

        [HttpGet]

        [Route("TotalPrice")]
       // /*[Authorize(Roles = "Customer")]*/
        public ActionResult<int> GetTotalPrice()

        {

            var result = cartRepo.GetToTalPrice(GetIdFromToken());

            return Ok(result);

        }

        private int GetIdFromToken()

        {

            var id = HttpContext.User.FindFirstValue("userId");

            return int.Parse(id);

        }

        [HttpPut]

        [Route("updateItem")]

        // [Authorize(Roles = "Customer")]

        public IActionResult UpdateItem(int productId, int quantity)

        {

           var result=cartRepo.UpdateItem(GetIdFromToken(), productId, quantity);

            return Ok(result);

        }

        [HttpDelete]

        [Route("EmptyCart")]

        // [Authorize(Roles = "Customer")]

        public IActionResult EmptyCart()

        {

            cartRepo.EmptyCart(GetIdFromToken());

            return Ok("Cart Empty");

        }

        [HttpGet]

        [Route("GetCount")]
        
      //   [Authorize(Roles = "Customer")]

        public ActionResult<int> TotalCount()

        {

            var result = cartRepo.GetCount(GetIdFromToken());

            return Ok(result);

        }

    }


}

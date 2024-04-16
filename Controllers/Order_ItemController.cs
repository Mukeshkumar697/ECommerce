using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Service;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_ItemsController : ControllerBase
    {
        private readonly IOrder_Item order_ItemRepo;
        private readonly ICart cartRepo;

        public Order_ItemsController(IOrder_Item order_ItemRepo, ICart cartRepo)
        {
            this.order_ItemRepo = order_ItemRepo;
            this.cartRepo = cartRepo;
        }
        [HttpPost]
       // [Authorize(Roles = "Customer")]
        public IActionResult AddOrderedItems(Guid orderId)
        {
            var id = int.Parse(HttpContext.User.FindFirst(c => c.Type == "UserId").Value);
            order_ItemRepo.AddOrderBillsItem(id, orderId);
            return Ok();
        }


        [HttpGet]
        [Route("getCartItems")]
      //  [Authorize(Roles = "Customer")]
        public IActionResult GetCartItems()
        {
            int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
            var result = cartRepo.GetAllCartItems(id);

            return Ok(result);
        }

        [HttpGet]
       // [Authorize(Roles = "Customer")]
        public IActionResult GetOrderedItems()
        {
            //int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
            var result = order_ItemRepo.GetOrderedItems();
            return Ok(result);
        }




    }
}

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
    [Authorize(Roles = "Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder order;

        public OrdersController(IOrder order)
        {
            this.order = order;
        }

        [HttpPost]
        [Route("AddOrder")]
       // [Authorize(Roles = "Customer")]
        public IActionResult BuyNow()
        {
            try
            {
                var result = order.BuyNow(GetIdFromToken());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("User id not exist");
            }

        }


        [HttpPost]
        [Route("AddOrderById")]
      //  [Authorize(Roles = "Customer")]
        public IActionResult BuyNowOrderById(int productId)
        {

            var result = order.BuyNowByOrderId(GetIdFromToken(), productId);
            return Ok(result);

        }


        [HttpGet]
        [Route("getNetAmount")]
     //   [Authorize(Roles = "Customer")]
        public int GetNetAmount()
        {

            var result = order.GetNetAmount(GetIdFromToken());
            return result;
        }

        private int GetIdFromToken()
        {
            var id = HttpContext.User.FindFirst("userId").Value;
            return int.Parse(id);
        }

        [HttpGet]
        [Route("getTotalAmount")]
      //  [Authorize(Roles = "Customer")]
        public int GetTotalAmount()
        {
            var result = order.GetTotalAmount(GetIdFromToken());
            return result;
        }
    }
}



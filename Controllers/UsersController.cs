using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Implementation;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {

        private readonly IUser userInterface;
        

        public UserController(IUser _userInterface )

        {

            userInterface = _userInterface;
            

        }



        [HttpGet]

       // [Authorize(Roles = "Admin")]

        public ActionResult<User> GetAllUsers()

        {

            var result = userInterface.GetAll();

            if (result != null)

            {

                return Ok(result);

            }

            return NotFound();

        }


        [HttpPut("{id}")]

        [Authorize(Roles = "Customer")]

        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            int id = int.Parse(HttpContext.User.FindFirst("userId").Value);

            var result = userInterface.UpdateUserDetail(id, user);

            if (result != null)

            {

                return Ok(result);

            }

            return BadRequest();

        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var result = userInterface.Login(email, password);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            var result = userInterface.AddUser(user);

            if (result != null)

            {

                return Ok(result);

            }

            return BadRequest("User already exist.Please login");

        }
    }
}




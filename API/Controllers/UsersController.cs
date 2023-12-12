using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Repositories;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UsersController : ControllerBase
    {
        //POST /api/users
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            User checkUser = UserRepository.AddUser(user);
            if (checkUser == null)
            {
                return BadRequest(
                    new
                    {
                        Error = new { status = "400", title = "Duplicate E-mail", detail = $"This E-mail address: ({user.Email}) is already in use" }
                    });
            }
            return Created("", $"User: {user.Name} created.");
        }
    }
}
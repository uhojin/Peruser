using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    
    public class UsersController : ControllerBase
    {
        private Database _db = new Database();

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (search != null)
            {
                return BadRequest(
                    new
                    {
                        Error = new { status = "400", title = "Duplicate E-mail", detail = $"This E-mail address: ({user.Email}) is already in use" }
                    });
            }
            else if (search != null && search.Listings == null)
            {
                search.Listings = new List<Listing>();
            }
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Created("", $"User: {user.Name} created.");
        }
    }
}
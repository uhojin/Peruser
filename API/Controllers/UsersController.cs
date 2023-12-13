using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Repositories;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UsersController : ControllerBase
    {
        // private Database _db = new Database();

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
        //POST /api/users/{userId}/listing
        [HttpPost("{userId}/listing")]
        public async Task<IActionResult> AddUserListing(Guid userId, Listing listing)
        {
            // Could be declared outside of this method so all methods can use the same instance
            var listingsRepository = new ListingsRepository();

            var result = await listingsRepository.AddListing(userId, listing);

            if (result == null)
                return NotFound(
                    new
                    {
                        Error = new
                        {
                            status = "400",
                            title = "User Not Found",
                            detail = $"User with ID:({userId}) does not exist"
                        }
                    });

            int userListingCount = listingsRepository.CountUserListings(userId).Result;

            // await _db.SaveChangesAsync();
            return Created("", new {title = listing.Title, total = userListingCount});
        }

        //POST /api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            Guid? token = UserRepository.login(user);
            if (token == null)
            {
                return BadRequest(
                    new
                    {
                        Error = new { status = "400", title = "Invalid Credentials", detail = $"Invalid Credentials" }
                    });
            }
            return Ok(new {userID = token});
        }
    }
}
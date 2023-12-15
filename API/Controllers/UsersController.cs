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
using API.Models.DTOs;
using System.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UsersController : ControllerBase
    {

        //GET /api/users/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            UserDTO user = await UserRepository.GetUserById(userId);
            if (user == null)
            {
                if (user == null)
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
            }
            return Ok(user);
        }

        //POST /api/users
        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] RegistrationDTO registration)
        {
            if (string.IsNullOrEmpty(registration.Password))
            {
                return BadRequest(new
                {
                    Success = false,
                    Error = new { status = "400", title = "Bad password", detail = $"This password: ({registration.Password}) is not valid"}
                });
            }
            User checkUser = await UserRepository.AddUser(registration.Email, registration.Name, registration.Password);
            if (checkUser == null)
            {
                return BadRequest(
                    new
                    {
                        Success = false,
                        Error = new { status = "400", title = "Duplicate Name or bad Email", detail = $"This E-mail address: ({registration.Email}) is not valid or this name already exists" }
                    });
            }
    
            return Ok(new {Success = true});
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
        public async Task<IActionResult> Login([FromBody] UserDTO dto)
        {
            Guid? token = await UserRepository.Login(dto.Name, dto.Password);
            if (token == null)
            {
                return BadRequest(
                    new
                    {
                        Success = false,
                        Error = new { status = "400", title = "Invalid Credentials", detail = $"Invalid Credentials" }
                    });
            }
            return Ok(new {userID = token, Success = true});
        }
    }
}
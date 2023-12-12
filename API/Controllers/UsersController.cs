using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UsersController : ControllerBase
    {
        // private Database _db = new();


        //POST /api/users
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            // var search = await _db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            // if (search != null)
            // {
            //     return BadRequest(
            //         new
            //         {
            //             Error = new { status = "400", title = "Duplicate E-mail", detail = $"This E-mail address: ({user.Email}) is already in use" }
            //         });
            // }
            // else if (search != null && search.Listings == null)
            // {
            //     search.Listings = new List<Listing>();
            // }
            // _db.Users.Add(user);
            // await _db.SaveChangesAsync();
            // return Created("", $"User: {user.Name} created.");
            return NotFound();
        }
        //POST /api/users/{userId}/listing
        [HttpPost("{userId}/listing")]
        public async Task<IActionResult> AddUserListing(Guid userId, Listing listing)
        {
            // var user = await _db.Users
            //                     .Include(x => x.Listings)
            //                     .SingleOrDefaultAsync(x => x.Id == userId);
            // if (user == null)
            // {
            //     return NotFound(
            //         new
            //         {
            //             Error = new
            //             {
            //                 status = "400",
            //                 title = "User Not Found",
            //                 detail = $"User with ID:({userId}) does not exist"
            //             }
            //         });
            // }

            // else if (user.Listings == null)
            // {
            //     user.Listings = new List<Listing>();
            // }
            // if (listing.PostingDate.Equals(DateTime.MinValue))
            // {
            //     listing.PostingDate = DateTime.Now;
            // }
            // user.Listings.Add(listing);
            // await _db.SaveChangesAsync();
            
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
    }
}
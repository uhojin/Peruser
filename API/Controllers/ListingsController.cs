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
    public class ListingsController : ControllerBase
    {
        private Database _db = new Database();
        Repositories.ListingsRepository _listingsRepository = new Repositories.ListingsRepository();

        //GET /api/listings
        [HttpGet]
        public async Task<IActionResult> GetListings()
        {
            // return Ok("Listings");
            return Ok(await _db.Listings.ToListAsync());
        }
        //GET /api/listings/{listingName}
        //Take query parameter s as a search string and return all listings that contain the search string in their name
        // [HttpGet("{listingName}")]
        [HttpGet("/search/{listingName}")]
        public async Task<IActionResult> SearchListingsByName([FromQuery]string listingName)
        {
            // return Ok("Listings");
            return Ok(await _db.Listings.Where(x => x.Title.ToLower().Contains(listingName.ToLower())).ToListAsync());
        }

        //PUT /api/listings/{listingId}
        [HttpPut("{listingId}")]
        public async Task<IActionResult> UpdateListing(Guid listingId, Listing listing)
        {
            var updatedListing = await _listingsRepository.UpdateListing(listingId, listing);
            return updatedListing != null ? Ok(updatedListing) : NotFound();
        }

        //GET /api/listings/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetListingsByUser(Guid userId)
        {
            var listings = await _listingsRepository.GetListingByUser(userId);
            return listings != null ? Ok(listings) : NotFound();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //GET /api/listings
        [HttpGet]
        public async Task<IActionResult> GetListings()
        {
            // return Ok("Listings");
            return Ok(await _db.Listings.ToListAsync());
        }
    }
}
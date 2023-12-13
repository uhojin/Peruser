using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ListingsRepository
    {
        private Database _db = new();
        public async Task<Listing> AddListing(Guid userId, Listing listing)
        {
            // get user from database and check if it exists
            var user = await _db.Users
                                .Include(x => x.Listings)
                                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            // If user does not have listings, create a new list of listings
            user.Listings ??= new List<Listing>();

            if (listing.PostingDate.Equals(DateTime.MinValue))
            {
                listing.PostingDate = DateTime.Now;
            }
            user.Listings.Add(listing);
            
            await _db.SaveChangesAsync();

            return listing;
        }

        // public async Task<Listing> GetListing(Guid listingId)
        // {
        //     // return Database.Listings.FirstOrDefault(x => x.Id == listingId);
        //     return await _db.Listings
        //                     .Include(x => x.Owner)
        //                     .Include(x => x.Offers)
        //                     .ThenInclude(x => x.Buyer)
        //                     .SingleOrDefaultAsync(x => x.Id == listingId);
        // }

        public async Task<int> CountUserListings(Guid userId)
        {
            // return Database.Listings.Count(x => x.Owner.Id == userId);
            return await _db.Listings.CountAsync(x => x.Owner.Id == userId);
        }

        public async Task<Listing> UpdateListing(Guid listingId, Listing listing)
        {
            var listingToUpdate = await _db.Listings.FirstOrDefaultAsync(x => x.Id == listingId);
            if (listingToUpdate == null)
            {
                return null;
            }

            listingToUpdate.Title = listing.Title;
            listingToUpdate.ImgUrl = listing.ImgUrl;
            listingToUpdate.Price = listing.Price;
            listingToUpdate.Description = listing.Description;

            await _db.SaveChangesAsync();

            return listingToUpdate;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;
using API.Models.DTOs;
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

            if (listing.OwnerId == null)
            {
                listing.OwnerId = user.Id;
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
            return await _db.Listings.CountAsync(x => x.OwnerId == userId);
        }

        public async Task<List<ListingDTO>> GetListingByUser(Guid userId)
        {
            // return Database.Listings.FirstOrDefault(x => x.Owner.Id == userId);
            var result = await _db.Listings
                            // .Include(x => x.Owner)
                            // .Include(x => x.Offers)
                            // .ThenInclude(x => x.Buyer)
                            .Where(x => x.OwnerId == userId)
                            .ToListAsync();

            // make anonymous object to avoid circular reference
            return result.Select(x => new ListingDTO
            {
                Id = x.Id,
                Title = x.Title,
                ImgUrl = x.ImgUrl,
                // Owner = new UserDTO
                // {
                //     Id = x.OwnerId,
                //     Name = x.Owner.Name,
                //     Email = x.Owner.Email
                // },
                OwnerId = x.OwnerId,
                PostingDate = x.PostingDate,
                Price = x.Price,
                Description = x.Description
            }).ToList();
        }

        public async Task<Listing> DeleteListing(Guid listingId)
        {
            var listingToDelete = await _db.Listings.FirstOrDefaultAsync(x => x.Id == listingId);
            if (listingToDelete == null)
            {
                return null;
            }

            _db.Listings.Remove(listingToDelete);
            await _db.SaveChangesAsync();

            return listingToDelete;
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
        public async Task<Listing> PurchaseListing(Guid listingId, Guid buyerId)
        {
            var listingToPurchase = await _db.Listings
                                            // .Include(x => x.Offers)
                                            .FirstOrDefaultAsync(x => x.Id == listingId);
            if (listingToPurchase == null)
            {
                return null;
            }

            var buyer = await _db.Users.FirstOrDefaultAsync(x => x.Id == buyerId);
            if (buyer == null)
            {
                return null;
            }
            buyer.Currency -= listingToPurchase.Price;
            var owner = await _db.Users.FirstOrDefaultAsync(x => x.Id == listingToPurchase.OwnerId);
            if (owner == null)
            {
                return null;
            }
            owner.Currency += listingToPurchase.Price;

            _db.Listings.Remove(listingToPurchase);

            await _db.SaveChangesAsync();

            return listingToPurchase;
        }
    }
}
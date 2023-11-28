using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Entities
{
    public class Offer
    {
        public Guid Id { get; set; }
        public Listing? Listing { get; set; }
        public DateTime OfferDate { get; set; }
        public User? Buyer { get; set; }
    }
}
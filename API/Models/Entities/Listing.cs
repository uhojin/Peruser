using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Entities
{
    public class Listing
    {
        public Guid Id { get; set; }
        public string ImgUrl { get; set; }
        public User? Owner { get; set; }
        public DateTime PostingDate { get; set; }
        public List<Offer>? Offers { get; set; }
        // We could add tags using the image recognition to create sort, BIG maybe
    }
}
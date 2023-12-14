using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs;

namespace API.Models.Entities
{
    public class Listing
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ImgUrl { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime PostingDate { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        public List<Offer>? Offers { get; set; }
    }
}
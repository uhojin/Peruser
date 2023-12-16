using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOs
{
    public class ListingDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public Guid? OwnerId { get; set; }
        // public UserDTO? OwnerId { get; set; }
        public DateTime PostingDate { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
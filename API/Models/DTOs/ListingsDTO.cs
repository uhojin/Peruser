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
        public UserDTO? Owner { get; set; }
        public DateTime PostingDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
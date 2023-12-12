using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace API.Models.Entities
{
    public class User
    {
        public Guid? Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Priviledged { get; }
        public List<Listing>? Listings { get; set; }
        public int? Currency { get; set; }
    }
}
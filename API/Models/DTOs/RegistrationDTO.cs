using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOs
{
    public class RegistrationDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
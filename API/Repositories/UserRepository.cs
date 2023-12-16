using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models.DTOs;
using API.Models.Entities;
using API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Repositories
{
    public class UserRepository
    {
        private static Database _db = new Database();
        public static async Task<UserDTO> GetUserById(Guid id) {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (search == null) return null;
            UserDTO dto = new()
            {
                Id = search.Id,
                Name = search.Name,
                Email = search.Email,
                Currency = search.Currency
            };
            return dto;

        }
        public static async Task<UserDTO> GetUserByName(string name) {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Name == name);
            if (search == null) return null;
            UserDTO dto = new()
            {
                Id = search.Id,
                Name = search.Name,
                Email = search.Email
            };
            return dto;
        }
        public static async Task<User> AddUser(string email, string name, string password) {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Name == name);
            
            if (DoesUsernameExist(name)) return null; 
            if (IsValidEmail(email) == false) return null;
            if (string.IsNullOrEmpty(password)) return null;
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Currency = 50,
                Priviledged = false,
                Password = password
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public static bool DoesUsernameExist(string username)
        {
            return _db.Users.Any(x => x.Name == username);
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith(".")) return false;
            else return true;
        }

        public static async Task<Guid> DeleteUser(Guid id) {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            // login a user return userID as token
            _db.Users.Remove(search);
            await _db.SaveChangesAsync();
            return id;
        }

        public static async Task<Guid?> Login(string name, string password) {
            var search = await _db.Users.FirstOrDefaultAsync(x => x.Name == name && x.Password == password);
            if (search == null) return null;
            // login a user return userID as token
            return search.Id;
        }
    }
}
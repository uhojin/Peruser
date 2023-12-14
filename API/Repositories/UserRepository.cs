using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs;
using API.Models.Entities;
using API.Persistence;

namespace API.Models.Repositories
{
    public class UserRepository
    {
        private static Database _db = new Database();
        public static async Task<UserDTO> GetUserById(Guid id) {
            var search = _db.Users.FirstOrDefault(x => x.Id == id);
            if (search == null) return null;
            UserDTO dto = new UserDTO {
                Id = search.Id,
                Name = search.Name,
                Email = search.Email
            };
            return dto;

        }
        public static async Task<UserDTO> GetUserByName(string name) {
            var search = _db.Users.FirstOrDefault(x => x.Name == name);
            if (search == null) return null;
            UserDTO dto = new UserDTO {
                Id = search.Id,
                Name = search.Name,
                Email = search.Email
            };
            return dto;
        }
        public static async Task<User> AddUser(User user) {
            var search = _db.Users.FirstOrDefault(x => x.Email == user.Email);
           
           if (DoesUsernameExist(user.Name))
            {
                return null; 
            }

            _db.Users.Add(user);
            _db.SaveChanges();
            return user;


        }

        public static bool DoesUsernameExist(string username)
        {
            return _db.Users.Any(x => x.Name == username);
        }

        public static void DeleteUser(User user) {
            // delete a user
        }

        public static Guid? login(User user) {
            // login a user return userID as token
            // use email and name to login for testing

            var search = _db.Users.FirstOrDefault(x => x.Email == user.Email && x.Name == user.Name);
            // if (search == null) {
            //     return null;
            // }
            // return search.Id;
            return search?.Id;

        }
    }
}
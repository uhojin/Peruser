using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Persistence;

namespace API.Models.Repositories
{
    public class UserRepository
    {
        private static Database _db = new Database();
        public static void GetUserById(Guid id) {
            // get a user
        }
        public static User AddUser(User user) {
            var search = _db.Users.FirstOrDefault(x => x.Email == user.Email);
            if(search != null) {
                _db.Users.Add(user);
                _db.SaveChanges();
                return user;
            }
            else return null;
        }
        public static void DeleteUser(User user) {
            // delete a user
        }
    }
}
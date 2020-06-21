using SourceControlAssignment.Models;
using System;
using System.Linq;

namespace SourceControlAssignment.Repository
{
    public class UserRepository
    {
        public User GetUser(Guid guid)
        {
            User user = new User();
            using (UserEntities db = new UserEntities())
            {
                user = db.Users.Where(x => x.rowguid == guid).FirstOrDefault();
            }
            return user;
        }
        public bool InsertUser(User user)
        {
            bool status = false;
            using (UserEntities db = new UserEntities())
            {
                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool IsEmailExist(string Email)
        {
            bool status = false;
            using (UserEntities db = new UserEntities())
            {
                User user = db.Users.Where(x => x.Email == Email).FirstOrDefault();
                if (user == null)
                {
                    status = true;
                }
            }
            return status;
        }

        public User IsUserAuthenticated(string Email, string Password)
        {
            User user = new User();
            using (UserEntities db = new UserEntities())
            {
                user = db.Users.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            }
            return user;
        }
    }
}
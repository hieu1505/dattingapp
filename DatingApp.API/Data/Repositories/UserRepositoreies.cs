using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;

namespace DatingApp.API.Data.Repositories
{
    public class UserRepositoreies : IUserRepositoreies
    {   
        private readonly DataContext _context;
        public UserRepositoreies(DataContext context){
            _context=context;
        }
       
        public User GetUserbyid(int id)
        {
            return _context.AppUsers.FirstOrDefault(user=>user.Id==id);
        }
        public User GetUserbyusername(string Username)
        {
            return _context.AppUsers.FirstOrDefault(user=>user.Username==Username);
        }

        public List<User> GetUsers()
        {
           return _context.AppUsers.ToList();
        }

        public void InsertNewUser(User user)
        {
           _context.AppUsers.Add(user);
        }

        public void UppdateUser(User user)
        {
           _context.AppUsers.Update(user);
        }

         public void DeleteUser(User user)
        {
           _context.AppUsers.Remove(user);
        }
        public bool IsSaveChanges(){
            return _context.SaveChanges()>0;
        }
    }
}
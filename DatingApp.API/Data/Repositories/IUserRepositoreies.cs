using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Database.Entities;

namespace DatingApp.API.Data.Repositories
{
    public interface IUserRepositoreies
    {
        List<User> GetUsers();
        User GetUserbyusername(string Username );
        User GetUserbyid(int id);
        void InsertNewUser(User user);
        void UppdateUser(User user);
        void DeleteUser(User user);
        bool IsSaveChanges();


    }
}
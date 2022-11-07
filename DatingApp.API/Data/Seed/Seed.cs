using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;

namespace DatingApp.API.Data.Seed
{
    public static class Seed
    {
        public static void SeedUser(DataContext contex){
            if(contex.AppUsers.Any()) return;
            var usercontext =System.IO.File.ReadAllText("Data/Seed/user.json");
            var users =JsonSerializer.Deserialize<List<User>> (usercontext);
            foreach(var user in users){
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@$$w0rd1"));
                user.PasswordSalt = hmac.Key;
                user.CreatedAt=DateTime.Now;
                contex.AppUsers.Add(user);
            }
            contex.SaveChanges();
        }
    }
}
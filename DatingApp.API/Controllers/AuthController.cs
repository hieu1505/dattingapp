using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AuthController:BaseController
    {
        private readonly DataContext _context;
        public AuthController(DataContext context){
                _context=context;
        }
        [HttpPost("register")]
         public IActionResult Register([FromBody] AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();
            if (_context.AppUsers.Any(u => u.Username == authUserDto.Username))
            {
                return BadRequest("Username is already existed!");
            }
 
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(authUserDto.Password);
            var newUser = new User {
                Username = authUserDto.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };
 
            _context.AppUsers.Add(newUser);
            _context.SaveChanges();
            return Ok(newUser.Username);
        }
        [HttpPost("login")]
        public void Login([FromBody] string value)
        {
        }

    }
}
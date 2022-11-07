using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data.Repositories;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {   
         private readonly IUserRepositoreies _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepositoreies userRepositoreies,ITokenService tokenService){
            _userRepository=userRepositoreies;
            _tokenService=tokenService;
        }
          
        public string Login(AuthUserDto authUserDto)
        {
              authUserDto.Username = authUserDto.Username.ToLower();
 
            var currentUser = _userRepository.GetUserbyusername(authUserDto.Username);
 
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("Username is invalid!");
            }
 
            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(authUserDto.Password));
            for (int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if (currentUser.PasswordHash[i] != passwordBytes[i])
                {
                    throw new UnauthorizedAccessException("Password is invalid!");
                }
            }
            return _tokenService.CreateToken(currentUser.Username);

        }

        public string Register(RegisterUserDto registerUserDto)
        {
            registerUserDto.Username = registerUserDto.Username.ToLower();
            var currentUser = _userRepository.GetUserbyusername(registerUserDto.Username);
            if (currentUser != null)
            {
                throw new BadHttpRequestException("Username is already registered");
            }
 
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDto.Password);
            var newUser = new User
            {
                Username = registerUserDto.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };
            _userRepository.InsertNewUser(newUser);
            _userRepository.IsSaveChanges();
 
            return _tokenService.CreateToken(newUser.Username);

        }
    }
}
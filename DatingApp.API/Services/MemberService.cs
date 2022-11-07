using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;

namespace DatingApp.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MemberService(DataContext context,IMapper mapper)

        {
            _mapper=mapper;
            _context = context;
        }
        public MemberDto GetMemberByUsername(string username)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.Username == username);
            // return new MemberDto
            // {
            //     Avatar = user.Avartar,
            //     City = user.city,
            //     CreatedAt = user.CreatedAt,
            //     DateOfBirth = user.DateofBirth,
            //     Email = user.Email,
            //     Gender = user.Gender,
            //     Introduction = user.Introduction,
            //     KnownAs = user.KnownsAs,
            //     Username = user.Username,
            // };
            return _mapper.Map<User ,MemberDto>(user);


        }

        public List<MemberDto> GetMembers()
        {
            // return _context.AppUsers
            //    .Select(user => new MemberDto
            //    {
            //        Avatar = user.Avartar,
            //        City = user.city,
            //        CreatedAt = user.CreatedAt,
            //        DateOfBirth = user.DateofBirth,
            //        Email = user.Email,
            //        Gender = user.Gender,
            //        Introduction = user.Introduction,
            //        KnownAs = user.KnownsAs,
            //        Username = user.Username,
            //    })
            //    .ToList();
            return _context.AppUsers.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToList();
        }
    }

}


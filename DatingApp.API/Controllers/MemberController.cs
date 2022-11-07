using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/member")]

    public class MemberController : BaseController
    {
        private readonly IMemberService _memberservice;
        public MemberController(IMemberService memberService)
        {
            _memberservice = memberService;
        }

        [HttpGet]

        public ActionResult<IEnumerable<MemberDto>> Get()
        {
            return Ok(_memberservice.GetMembers());
        }

        [HttpGet("{username}")]
        public ActionResult<MemberDto> Get(string username)
        {
            var member = _memberservice.GetMemberByUsername(username);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Database.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id {get;set;}
        [Required]
        [MaxLength(256)]
        public string Username {get;set;}
        [MaxLength(256)]
        public string Email {get; set;}
        public byte[] PasswordHash  {get;set;}
        public byte[] PasswordSalt  {get;set;}

        public DateTime? DateofBirth { get; set; }
        [MaxLength(32)]
        public string KnownsAs{get;set;}
        [MaxLength(6)]
        public string Gender { get; set; }
        [MaxLength(32)]
        public string city { get; set; }
        [MaxLength(256)]
        public string Introduction { get; set; }
        [MaxLength(256)]
        public string Avartar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
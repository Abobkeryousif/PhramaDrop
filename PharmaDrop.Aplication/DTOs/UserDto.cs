using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime BirthDay { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }



    public record GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime BirthDay { get; set; }
    } 

    public record UpdateUserDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public string Password { get; set; }
    }

        
    }

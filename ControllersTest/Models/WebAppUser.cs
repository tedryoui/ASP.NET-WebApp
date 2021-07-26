using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ControllersTest.Models
{
    public class WebAppUser : IdentityUser
    {
        public bool Blocked { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegDate { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime LogDate { get; set; }
    }
}
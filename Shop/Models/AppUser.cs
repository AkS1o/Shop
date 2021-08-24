using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string FacebookURL { get; set; }
        public string TelegramURL { get; set; }
    }
}

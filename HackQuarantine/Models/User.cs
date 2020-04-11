using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }

        public User()
        {

        }

        public User(string fullName, string email, string password, string address)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Address = address;
        }
    }
}

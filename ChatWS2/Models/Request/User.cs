using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatWS2.Models.Request
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.DomainModel
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplication.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

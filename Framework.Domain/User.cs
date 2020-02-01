using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public class User
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }
        public int Id { get; private set; }

        public User(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public void DeactiveUser()
        {
            this.IsActive = false;
        }
    }
}

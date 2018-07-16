using System;
using System.Collections.Generic;
using System.Text;

namespace ClixMart_MobileApp
{
    public class User
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public User(){}
        public User(string username, string email, string pass)
        {
            this.Username = username;
            this.Email = email;
            this.Password = pass;
        }
        public User(string userId, string username, string email, string pass)
        {
            this.UserID = userId;
            this.Username = username;
            this.Email = email;
            this.Password = pass;
        }

        public override string ToString()
        {
            return "UserID:" + UserID + " Username:" + Username + " Email:" + Email + " Password:" + Password;
        }
    }
}

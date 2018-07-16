using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ClixMart_MobileApp.Model;
using SQLite;

namespace ClixMart_MobileApp.Controller
{
    public class UserLocalController
    {
        private string DB_NAME = "ClixMart.db3";
        private string KEY = "HAKUNAMATATA";
        private SQLiteConnection conn;

        public UserLocalController()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DB_NAME);
            conn = new SQLiteConnection(path);
            conn.CreateTable<UserLocal>();
        }

        public void AddUser(string username, string pass)
        {
            conn.Insert(new UserLocal() {
                Id = KEY,
                userName = username,
                password = pass
            });
        }

        public void DeleteUser(UserLocal userLocal)
        {
            conn.Delete(userLocal);
        }

        public void UpdateUser(UserLocal userLocal)
        {
            conn.Update(userLocal);
        }

        public UserLocal DataAccess()
        {
            UserLocal user = null;
            IEnumerable<UserLocal> users = conn.Query<UserLocal>("select * from UserLocal where Id = ?", KEY);
            if(users.Count() > 0)
            {
                user = users.ToList()[0];
            }
            return user;
        }
    }
}

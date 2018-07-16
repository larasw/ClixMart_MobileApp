using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClixMart_MobileApp.Controller
{
    public class UserController
    {
        public void updateUser(User user, string type, string param)
        {
            DatabaseConnection db = new DatabaseConnection();
            string query = null;

            if (type == "username") query = "update user set userName=@param where userId=@userId";
            else if (type == "email") query = "update user set Email=@param where userId=@userId";
            else if (type == "password") query = "update user set Password=@param where userId=@userId";

            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@param", param);
            command.Parameters.AddWithValue("@userId", user.UserID);
            command.ExecuteNonQuery();
            db.Close();

            if (type == "username") user.Username = param;
            else if (type == "email") user.Email = param;
            else if (type == "password") user.Password = param;
        }
    }
}

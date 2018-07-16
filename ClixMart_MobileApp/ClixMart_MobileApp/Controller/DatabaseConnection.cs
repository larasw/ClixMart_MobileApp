using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ClixMart_MobileApp
{
    public class DatabaseConnection
    {
        public MySqlConnection Conn { get; set; }

        public void Open()
        {
            try
            {
                string connString = "Server=studmysql01.fhict.local;uid=dbi341965;Database=dbi341965;pwd=Fontys123;charset=utf8;SslMode=none";
                Conn = new MySqlConnection(connString);
                Conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Message: " + e.Message);
            }
        }

        public void Close()
        {
            Conn.Close();
        }
    }
}

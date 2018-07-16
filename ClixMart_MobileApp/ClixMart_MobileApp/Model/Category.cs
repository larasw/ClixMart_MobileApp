using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClixMart_MobileApp
{
    public class Category
    {
        public string CategoryID { get; }
        public string CategoryName { get; }

        public Category() { }

        public Category(string id, string name)
        {
            this.CategoryID = id;
            this.CategoryName = name;
        }
    }
}

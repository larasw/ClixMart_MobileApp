using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClixMart_MobileApp.Controller
{
    public class CategoryController
    {
        public List<Item> GetListOfItems(Category category)
        {
            DatabaseConnection db = new DatabaseConnection();
            List<Item> items = new List<Item>();

            string query = "select * from item where Category_Category_id=@category_id;";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@category_id", category.CategoryID);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item(reader["itemId"].ToString(), reader["itemName"].ToString(), reader["item_picture"].ToString()));
            }
            db.Close();

            return items;
        }
    }
}

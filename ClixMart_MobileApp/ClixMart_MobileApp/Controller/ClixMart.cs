using ClixMart_MobileApp.Controller;
using ClixMart_MobileApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClixMart_MobileApp
{
    public class ClixMart
    {
        private DatabaseConnection db = new DatabaseConnection();
        private UserLocalController localController = new UserLocalController();
        private UserLocal userLocal = null;
        public User User { get; set; }

        public ClixMart() { }

        public bool Login(string username, string pass)
        {
            bool valid = false;
            // Check from database
            
            string query = "select * from user where userName=@username and password=@password;";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", pass);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["userId"] != null)
                {
                    User = new User(reader["userId"].ToString(), username, reader["Email"].ToString(), pass);
                    valid = true;
                    if(userLocal == null)
                    {
                        localController.AddUser(username, pass);
                        userLocal = CheckData();
                    }
                }
            }
            db.Close();
            
            return valid;
        }

        public bool CheckValidity(User user)
        {
            bool valid = false;
            string query = "select * from user where userName=@username or Email=@email;";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@email", user.Email);
            MySqlDataReader reader = command.ExecuteReader();

            if(!reader.Read())
            {
                valid = true;
            }

            db.Close();
            return valid;
        }

        public void SignUp(User user)
        {
            string query = "insert into user (userName, Email, Password) values (@username, @email, @password);";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.ExecuteNonQuery();
            db.Close();

            string getID = "select userId from user where userName=@username;";
            db.Open();
            command = new MySqlCommand(getID, db.Conn);
            command.Parameters.AddWithValue("@username", user.Username);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.UserID = reader["userId"].ToString();
                User = user;
                if (userLocal == null)
                {
                    localController.AddUser(user.Username, user.Password);
                    userLocal = CheckData();
                }
            } 
            db.Close();
        }

        public Item SearchItem(string keyword)
        {
            return null;
        }

        public List<Category> GetListOfCategories()
        {
            List<Category> categories = new List<Category>();
            
            string query = "select * from category;";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category(reader["Category_id"].ToString(), reader["Category_name"].ToString()));
            }
            db.Close();
            
            return categories;
        }

        public List<Item> GetFavoriteItemList()
        {
            List<string> itemId = GetFavoriteItemID();
            List<Item> items = new List<Item>();

            string getItems = "select * from item where itemId=@itemId;";

            for (int i=0; i<itemId.Count; i++)
            {
                db.Open();
                MySqlCommand command = new MySqlCommand(getItems, db.Conn);
                command.Parameters.AddWithValue("@itemId", itemId[i]);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new Item(reader["itemId"].ToString(), reader["itemName"].ToString(), reader["item_picture"].ToString()));
                }
                db.Close();
            }
            
            return items;
        }

        public List<string> GetFavoriteItemID()
        {
            List<string> itemId = new List<string>();

            string getItemId = "select itemId from tracking where userId=@userId;";

            db.Open();
            MySqlCommand command = new MySqlCommand(getItemId, db.Conn);
            command.Parameters.AddWithValue("@userId", User.UserID);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                itemId.Add(reader["itemId"].ToString());
            }
            db.Close();

            return itemId;
        }

        public void AddFavoriteItem(string itemId)
        {
            string query = "insert into tracking (userID, itemId) values (@userId, @itemId);";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@userId", User.UserID);
            command.Parameters.AddWithValue("@itemId", itemId);
            command.ExecuteNonQuery();
            db.Close();
        }

        public void DeleteFavoriteItem(string itemId)
        {
            string query = "delete from tracking where userId=@userId and itemId=@itemId;";
            db.Open();
            MySqlCommand command = new MySqlCommand(query, db.Conn);
            command.Parameters.AddWithValue("@userId", User.UserID);
            command.Parameters.AddWithValue("@itemId", itemId);
            command.ExecuteNonQuery();
            db.Close();
        }

        public List<PriceTemp> GetItemPriceList(string itemId)
        {
            List<PriceTemp> prices = new List<PriceTemp>();

            string getItemId = "select * from brand where itemId=@itemId;";

            db.Open();
            MySqlCommand command = new MySqlCommand(getItemId, db.Conn);
            command.Parameters.AddWithValue("@itemId", itemId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                prices.Add(new PriceTemp(
                    reader["brandName"].ToString(),
                    Convert.ToDouble(reader["ahPrice"].ToString()),
                    Convert.ToDouble(reader["jumboPrice"].ToString()),
                    Convert.ToDouble(reader["lidlPrice"].ToString())
                    ));
            }
            db.Close();

            return prices;
        }

        public double[] GetCheapestPrice(List<PriceTemp> priceList)
        {
            double[] cheapList = new double[3];
            List<double> ahList = new List<double>();
            List<double> jumboList = new List<double>();
            List<double> lidlList = new List<double>();

            for (int i = 0; i < priceList.Count; i++)
            {
                ahList.Add(priceList[i].AHPrice);
                jumboList.Add(priceList[i].JumboPrice);
                lidlList.Add(priceList[i].LidlPrice);
            }

            cheapList[0] = GetCheapList(ahList);
            cheapList[1] = GetCheapList(jumboList);
            cheapList[2] = GetCheapList(lidlList);

            return cheapList;
        }

        private double GetCheapList(List<double> list)
        {
            double cheap = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if(i == 0)
                {
                    cheap = list[i];
                }
                else
                {
                    if(list[i] < cheap)
                    {
                        cheap = list[i];
                    }
                }
            }
            return cheap;
        }

        public UserLocal CheckData()
        {
            userLocal = localController.DataAccess();
            return userLocal;
        }

        public void DeleteUserLocal()
        {
            localController.DeleteUser(userLocal);
        }

        public void UpdateUserData(string type, string param)
        {
            if(type == "password")
            {
                userLocal.password = param;
            } else if(type == "username")
            {
                userLocal.userName = param;
            }

            localController.UpdateUser(userLocal);
        }
    }
}

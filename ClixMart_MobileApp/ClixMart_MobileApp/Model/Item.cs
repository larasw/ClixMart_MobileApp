using System;
using System.Collections.Generic;
using System.Text;

namespace ClixMart_MobileApp
{
    public class Item
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPicture { get; set; }

        public Item() { }
        public Item(string id, string name, string picture)
        {
            this.ItemId = id;
            this.ItemName = name;
            this.ItemPicture = picture;
        }
    }
}

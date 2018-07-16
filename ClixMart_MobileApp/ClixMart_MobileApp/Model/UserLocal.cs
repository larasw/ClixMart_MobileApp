using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ClixMart_MobileApp.Model
{
    [Table("UserLocal")]
    public class UserLocal
    {
        [PrimaryKey, MaxLength(12)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string userName { get; set; }
        [MaxLength(50)]
        public string password { get; set; }
    }
}

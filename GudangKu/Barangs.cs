using SQLite;
using System;

namespace GudangKu
{
    public class Barangs
    {
        [PrimaryKey, AutoIncrement]
        public int inventory_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

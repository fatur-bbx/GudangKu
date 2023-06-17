using SQLite;
using System;

namespace GudangKu
{
    public class Transaksis
    {
        [PrimaryKey, AutoIncrement]
        public int transaction_id { get; set; }
        public int inventory_id { get; set; }
        public string transaction_type { get; set; }
        public int quantity { get; set; }
        public DateTime transaction_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

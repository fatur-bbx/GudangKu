using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace GudangKu
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Barangs>();
            _database.CreateTableAsync<Transaksis>();
        }


        // Barangs
        public Task<List<Barangs>> GetBarangAsync()
        {
            return _database.Table<Barangs>().ToListAsync();
        }

        public Task<List<KeyValuePair<int, string>>> GetBarangAndIdAsync()
        {
            return _database.Table<Barangs>().ToListAsync()
                .ContinueWith(task =>
                {
                    List<Barangs> barangList = task.Result;
                    return barangList.Select(barang => new KeyValuePair<int, string>(barang.inventory_id, barang.name)).ToList();
                });
        }

        public Task<Barangs> GetBarangByID(int id_barang)
        {
            return _database.Table<Barangs>().FirstOrDefaultAsync(barang => barang.inventory_id == id_barang);
        }

        public Task<int> SaveBarangAsync(Barangs barangs)
        {
            return _database.InsertAsync(barangs);
        }

        public Task<int> UpdateBarangAsync(Barangs barangs)
        {
            return _database.UpdateAsync(barangs);
        }

        public Task<int> DeleteBarangAsync(Barangs barangs)
        {
            return _database.DeleteAsync(barangs);
        }


        // Transaksis
        public async Task<List<TransaksiWithBarang>> GetTransaksiAsync()
        {
            List<Transaksis> transaksiList = await _database.Table<Transaksis>().ToListAsync();

            List<TransaksiWithBarang> transaksiWithBarangList = new List<TransaksiWithBarang>();

            foreach (Transaksis transaksi in transaksiList)
            {
                Barangs barang = await _database.FindAsync<Barangs>(transaksi.inventory_id);
                transaksiWithBarangList.Add(new TransaksiWithBarang
                {
                    Transaksi = transaksi,
                    NamaBarang = barang?.name // Jika barang tidak ditemukan, NamaBarang akan menjadi null
                });
            }

            return transaksiWithBarangList;
        }


        public Task<int> SaveTransaksiAsync(Transaksis transaksi)
        {
            return _database.InsertAsync(transaksi);
        }

        public Task<int> UpdateTransaksiAsync(Transaksis transaksi)
        {
            return _database.UpdateAsync(transaksi);
        }

        public Task<int> DeleteTransaksiAsync(Transaksis transaksi)
        {
            return _database.DeleteAsync(transaksi);
        }

        public Task<int> DeleteTransaksiWithBarang(TransaksiWithBarang transaksi)
        {
            return _database.DeleteAsync(transaksi);
        }
    }
}

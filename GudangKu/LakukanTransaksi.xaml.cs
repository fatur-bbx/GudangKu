using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GudangKu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LakukanTransaksi : ContentPage
    {
        public LakukanTransaksi()
        {
            InitializeComponent();
        }

        private int selectedBarangId;
        Barangs selectedBarang;

        private void NamaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mendapatkan ID barang terpilih dari Picker
            selectedBarangId = ((KeyValuePair<int, string>)namaPicker.SelectedItem).Key;
            id_barang_acuan.Text = selectedBarangId.ToString();
            
        }


        private async void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                bool isValid = int.TryParse(e.NewTextValue, out int value);
                Barangs selectedBarang = await App.Database.GetBarangByID(selectedBarangId);
                if (!isValid || value < 0 || value > selectedBarang.quantity)
                {
                    ((Entry)sender).Text = e.OldTextValue;
                    await DisplayAlert("Peringatan", "Tidak bisa lebih dari "+selectedBarang.quantity+"!", "OK");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Mengambil daftar barang dari database
            List<KeyValuePair<int, string>> barangList = await App.Database.GetBarangAndIdAsync();

            // Mengisi Picker dengan daftar barang
            namaPicker.ItemsSource = barangList;
            namaPicker.ItemDisplayBinding = new Binding("Value"); // Menampilkan nama barang sebagai teks
        }


        private async void TambahTransaksi(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(namaPicker.SelectedItem?.ToString())||
                string.IsNullOrEmpty(jenisTransaksi.SelectedItem?.ToString())||
                string.IsNullOrEmpty(jumlahEntry.Text)){
                await DisplayAlert("Peringatan", "Semua field harus diisi", "OK");
            }
            else
            {
                await App.Database.SaveTransaksiAsync(new Transaksis
                {
                    inventory_id = int.Parse(id_barang_acuan.Text),
                    transaction_type = jenisTransaksi.SelectedItem.ToString(),
                    quantity = int.Parse(jumlahEntry.Text),
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                });
                Task<Barangs> brg = App.Database.GetBarangByID(selectedBarangId);
                await App.Database.UpdateBarangAsync(new Barangs
                {
                    inventory_id = brg.Result.inventory_id,
                    name = brg.Result.name,
                    description = brg.Result.description,
                    price = brg.Result.price,
                    quantity = brg.Result.quantity - int.Parse(jumlahEntry.Text),
                    updated_at = DateTime.Now
                });
                await DisplayAlert("Pemberitahuan", "Transaksi berhasil ditambah!", "OK");
                namaPicker.SelectedIndex = 0;
                jenisTransaksi.SelectedIndex = 0;
                jumlahEntry.Text = "";
            }
        }
    }
}
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
    public partial class UbahBarang : ContentPage
    {
        Barangs barang;
        int id;
        public UbahBarang(Barangs brg)
        {
            InitializeComponent();
            barang = brg;
            id = barang.inventory_id;
            namaEntry.Text = barang.name;
            deskripsiEntry.Text = barang.description;
            jumlahEntry.Text = barang.quantity.ToString();
            hargaEntry.Text = barang.price.ToString();
        }

        private async void UbahButton_Clicked(object sender, EventArgs e)
        {
            // Validasi inputan
            if (string.IsNullOrWhiteSpace(namaEntry.Text) ||
                string.IsNullOrWhiteSpace(deskripsiEntry.Text) ||
                string.IsNullOrWhiteSpace(jumlahEntry.Text) ||
                string.IsNullOrWhiteSpace(hargaEntry.Text))
            {
                // Tampilkan peringatan jika ada input yang kosong
                await DisplayAlert("Peringatan", "Semua field harus diisi", "OK");
            }
            else
            {
                await App.Database.UpdateBarangAsync(new Barangs
                {
                    inventory_id = id,
                    name = namaEntry.Text,
                    description = deskripsiEntry.Text,
                    quantity = int.Parse(jumlahEntry.Text),
                    price = int.Parse(hargaEntry.Text),
                    updated_at = DateTime.Now
                });
                await DisplayAlert("Sukses", "Data berhasil diubah", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
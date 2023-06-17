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
    public partial class TambahBarang : ContentPage
    {
        public TambahBarang()
        {
            InitializeComponent();
        }

        private async void SimpanButton_Clicked(object sender, EventArgs e)
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
                await App.Database.SaveBarangAsync(new Barangs
                {
                    name = namaEntry.Text,
                    description = deskripsiEntry.Text,
                    quantity = int.Parse(jumlahEntry.Text),
                    price = int.Parse(hargaEntry.Text),
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                });
                // Tampilkan pesan sukses
                await DisplayAlert("Sukses", "Data berhasil disimpan", "OK");

                // Reset form
                namaEntry.Text = "";
                deskripsiEntry.Text = "";
                jumlahEntry.Text = "";
                hargaEntry.Text = "";
            }
        }
    }
}
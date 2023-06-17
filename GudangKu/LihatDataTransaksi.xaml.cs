using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GudangKu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LihatDataTransaksi : ContentPage
    {
        public LihatDataTransaksi()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetTransaksiAsync();
        }

        private async void delete(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Konfirmasi", "Apakah anda ingin menghapus Transaksi ini?", "Ya", "Tidak");

            if (result)
            {
                var button = (Button)sender;
                var trans = button.BindingContext as TransaksiWithBarang;
                await App.Database.DeleteTransaksiAsync(trans.Transaksi);
                await DisplayAlert("Pemberitahuan", "Berhasil menghapus Transaksi!", "OK");
                collectionView.ItemsSource = await App.Database.GetTransaksiAsync();
            }
        }
    }
}
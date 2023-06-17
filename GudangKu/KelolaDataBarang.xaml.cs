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
    public partial class KelolaDataBarang : ContentPage
    {
        public ObservableCollection<Barang> DataBarang { get; set; }
        public KelolaDataBarang()
        {
            InitializeComponent();
        }

        private async void HapusDataBarang(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Konfirmasi", "Apakah anda ingin menghapus Barang ini?", "Ya", "Tidak");

            if (result)
            {
                var button = (Button)sender;
                var barang = button.BindingContext as Barangs;
                await App.Database.DeleteBarangAsync(barang);
                await DisplayAlert("Pemberitahuan", "Berhasil menghapus Barang!", "OK");
                collectionView.ItemsSource = await App.Database.GetBarangAsync();
            }
        }

        private async void EditDataBarang(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Konfirmasi", "Apakah anda ingin melanjutkan perubahan data Barang ini?", "Ya", "Tidak");

            if (result)
            {
                var button = (Button)sender;
                var barang = button.BindingContext as Barangs;
                await Navigation.PushAsync(new UbahBarang(barang));
            }
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetBarangAsync();
        }
    }
}
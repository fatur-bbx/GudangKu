using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GudangKu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LihatDataBarang : ContentPage
    {
        public ObservableCollection<Barang> DataBarang { get; set; }

        public LihatDataBarang()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetBarangAsync();
        }
    }

    public class Barang
    {
        public string Nama { get; set; }
        public int Jumlah { get; set; }
        public string Deskripsi { get; set; }
        public double HargaPerPcs { get; set; }
    }
}

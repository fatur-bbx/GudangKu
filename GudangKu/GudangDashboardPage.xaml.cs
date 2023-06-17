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
    public partial class GudangDashboardPage : ContentPage
    {
        public GudangDashboardPage()
        {
            InitializeComponent();
        }

        private async void LihatDataBarang(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LihatDataBarang());
        }

        private async void TambahDataBarang(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TambahBarang());
        }

        private async void KelolaDataBarang(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KelolaDataBarang());
        }
    }
}
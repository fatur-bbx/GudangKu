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
    public partial class TransaksiDashboardPage : ContentPage
    {
        public TransaksiDashboardPage()
        {
            InitializeComponent();
        }

        private async void LakukanTransaksiOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LakukanTransaksi());
        }

        private async void LihatDataTransaksiOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LihatDataTransaksi());
        }
    }
}
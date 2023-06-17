using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GudangKu
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "inventaris.db3"));
                }

                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using Delfos.Database;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Delfos.Views;

namespace Delfos
{
    public partial class App : Application
    {
        private static DB database;

        public static DB Database
        {
            get
            {
                if (database == null)
                {
                    string databasePath = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), "xamarinDB.db");
                    Debug.WriteLine(databasePath);
                    database = new DB(databasePath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

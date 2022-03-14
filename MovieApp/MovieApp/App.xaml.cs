using MovieApp.Services;
using MovieApp.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieApp
{
    public partial class App : Application
    {

        private static LocalDataBase dbContext;

        public static LocalDataBase DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    string dbPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "ShoppingListDatabase-v01.db3");

                    dbContext = new LocalDataBase(dbPath);
                }

                return dbContext;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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

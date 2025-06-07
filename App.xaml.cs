using Microsoft.Maui.Storage;
using Elecciones.Views;
﻿namespace Elecciones
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var storedUser = Preferences.Get("username", null);
            if (!string.IsNullOrEmpty(storedUser))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        protected override void OnStart()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }

    }
}

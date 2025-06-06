namespace Elecciones
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }

    }
}

using IndustrialEnterpriseApp.Pages;

namespace IndustrialEnterpriseApp

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
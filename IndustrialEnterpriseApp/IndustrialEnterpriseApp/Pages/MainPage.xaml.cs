using System;
using Microsoft.Maui.Controls;

namespace IndustrialEnterpriseApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Console.WriteLine("MainPage initialized");
        }

        private async void OnEmployeesClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Employees button clicked");
            await Navigation.PushAsync(new EmployeesPage());
        }

        private async void OnDepartmentsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Departments button clicked");
            await Navigation.PushAsync(new DepartmentsPage());
        }

        private async void OnPositionsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Positions button clicked");
            await Navigation.PushAsync(new PositionsPage());
        }
    }
}

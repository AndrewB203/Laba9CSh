using IndustrialEnterpriseApp.Data;
using IndustrialEnterpriseApp.Models;
using IndustrialEnterpriseApp.Services;
using System;
using System.Linq;

namespace IndustrialEnterpriseApp.Pages
{
    public partial class PositionsPage : ContentPage
    {
        private readonly PositionService _positionService;
        private Position _selectedPosition;

        public PositionsPage()
        {
            InitializeComponent();
            _positionService = new PositionService(new IndustrialEnterpriseContext());
            LoadPositions();
        }

        private void LoadPositions()
        {
            PositionsListView.ItemsSource = _positionService.GetAllPositions();
        }

        private async void OnAddPositionClicked(object sender, EventArgs e)
        {
            var position = new Position
            {
                Title = "New Position",
                Salary = 50000
            };

            _positionService.AddPosition(position);
            LoadPositions();
        }

        private async void OnEditPositionClicked(object sender, EventArgs e)
        {
            if (_selectedPosition != null)
            {
                _selectedPosition.Title = "Updated Position";
                _positionService.UpdatePosition(_selectedPosition);
                LoadPositions();
            }
        }

        private async void OnDeletePositionClicked(object sender, EventArgs e)
        {
            if (_selectedPosition != null)
            {
                _positionService.DeletePosition(_selectedPosition.Id);
                LoadPositions();
            }
        }

        private void PositionsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedPosition = e.SelectedItem as Position;
        }
    }
}
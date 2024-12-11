using HRDepartmentApp.Models;
using HRDepartmentApp.ViewModels;

namespace HRDepartmentApp.Views;

public partial class DepartmentView : ContentPage
{
    private readonly DepartmentViewModel _viewModel;

    public DepartmentView(DepartmentViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void AddDepartment_Clicked(object sender, EventArgs e)
    {
        var department = new Department
        {
            Name = "New Department",
            Location = "New Location"
        };
        await _viewModel.AddDepartmentAsync(department);
    }

    private async void UpdateDepartment_Clicked(object sender, EventArgs e)
    {
        if (DepartmentListView.SelectedItem is Department selectedDepartment)
        {
            selectedDepartment.Name = "Updated Department";
            await _viewModel.UpdateDepartmentAsync(selectedDepartment);
        }
    }

    private async void DeleteDepartment_Clicked(object sender, EventArgs e)
    {
        if (DepartmentListView.SelectedItem is Department selectedDepartment)
        {
            await _viewModel.DeleteDepartmentAsync(selectedDepartment.Id);
        }
    }
}
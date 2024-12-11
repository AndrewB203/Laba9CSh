using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;
namespace TodoSQLite.Views;

[QueryProperty("Position", "Position")]
public partial class PositionPage : ContentPage
{
    private readonly PositionRepository _repository;
    public Position Position
    {
        get => BindingContext as Position;
        set => BindingContext = value;
    }

    public PositionPage(PositionRepository repository)
    {
        InitializeComponent();
        _repository = repository;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Position.Title))
        {
            await DisplayAlert("Title Required", "Please enter a title for the position.", "OK");
            return;
        }

        await _repository.SavePositionAsync(Position);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Position.ID == 0)
            return;
        await _repository.DeletePositionAsync(Position);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
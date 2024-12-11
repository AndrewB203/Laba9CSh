using TodoSQLite.Data;
using TodoSQLite.Models;

namespace TodoSQLite.Views;

[QueryProperty("Position", "Position")]
public partial class PositionPage : ContentPage
{
    Position position;
    public Position Position
    {
        get => BindingContext as Position;
        set => BindingContext = value;
    }
    TodoItemDatabase database;
    public PositionPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Position.Title))
        {
            await DisplayAlert("Title Required", "Please enter a title for the position.", "OK");
            return;
        }

        await database.SavePositionAsync(Position);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Position.ID == 0)
            return;
        await database.DeletePositionAsync(Position);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
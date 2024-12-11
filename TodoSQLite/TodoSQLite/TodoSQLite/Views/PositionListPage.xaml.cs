using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;

namespace TodoSQLite.Views;

public partial class PositionListPage : ContentPage
{
    TodoItemDatabase database;
    public ObservableCollection<Position> Positions { get; set; } = new();
    public PositionListPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var positions = await database.GetPositionsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Positions.Clear();
            foreach (var position in positions)
                Positions.Add(position);
        });
    }

    async void OnPositionAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PositionPage), true, new Dictionary<string, object>
        {
            ["Position"] = new Position()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Position position)
            return;

        await Shell.Current.GoToAsync(nameof(PositionPage), true, new Dictionary<string, object>
        {
            ["Position"] = position
        });
    }
}
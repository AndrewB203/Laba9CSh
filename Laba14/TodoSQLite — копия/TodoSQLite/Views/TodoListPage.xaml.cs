using System.Collections.ObjectModel;
using TodoSQLite.Data;
using TodoSQLite.Models;
using TodoSQLite.Repositories;

namespace TodoSQLite.Views;

public partial class TodoListPage : ContentPage
{
    private readonly TodoItemRepository _repository;
    public ObservableCollection<TodoItem> Items { get; set; } = new();

    public TodoListPage(TodoItemRepository repository)
    {
        InitializeComponent();
        _repository = repository;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await _repository.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        });
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new TodoItem()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}

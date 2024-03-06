using ToDo.Data;
using ToDo.Models;


namespace ToDo.Views;

[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
    public TodoItem Item
    {
        get => BindingContext as TodoItem;
        set => BindingContext = value;
    }
    TodoItemDatabase database;

    public TodoItemPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Skriv in ärende", "Du måste skriv namn på ärendet.", "OK");
            return;
        }
        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }


    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.Id == 0)
        {

        }
        await database.DeleteItemAsync(Item);
        await Shell.Current?.GoToAsync("..");
    }
    async void OnCancelClicked(object sender, EventArgs e)
    {
        if (Item.Id == 0)
        {

        }
        await Shell.Current?.GoToAsync("..");
    }


}

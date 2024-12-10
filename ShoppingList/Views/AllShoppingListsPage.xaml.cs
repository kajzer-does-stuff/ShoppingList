namespace ShoppingList.Views;

public partial class AllShoppingListsPage : ContentPage
{
	public AllShoppingListsPage()
	{
		InitializeComponent();
		BindingContext = new Models.AllShoppingLists();
    }
    protected override void OnAppearing()
    {
        ((Models.AllShoppingLists)BindingContext).LoadAllShoppingLists();
    }
    private async void AddShoppingList_Clicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new NewShoppingListPage());
    }
    private async void ListSelection_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var selectedShoppingList = (Models.ShoppingList)e.CurrentSelection[0];
            await Shell.Current.GoToAsync($"{nameof(ShoppingListPage)}?{nameof(ShoppingListPage.ItemId)}={selectedShoppingList.ListFileName}");
            ListOfShoppingLists.SelectedItem = null;
        }
    }
}
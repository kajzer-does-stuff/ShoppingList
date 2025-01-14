using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class NewShoppingListPage : ContentPage
{
	public NewShoppingListPage()
	{
		InitializeComponent();
	}

    private async void AddNewShoppingList_Clicked(object sender, EventArgs e)
    { 
        string shoppingListName = UtilityThings.ValidateRegexExtra(NewShoppingListName_Input.Text, "Lista zakupów", @".+");

        string savePath = $"{FileSystem.AppDataDirectory}/{shoppingListName}.{Path.GetRandomFileName()}.shoppingList.json";

        Models.ShoppingList newShoppingList = new Models.ShoppingList()
        {
            ListName = shoppingListName,
            ListFileName = savePath,
            ListID = new Random().Next(),
            ItemsList = new ObservableCollection<ShoppingListItem>()
        };

        Models.ShoppingList.SaveShoppingList(savePath, newShoppingList);

        await Shell.Current.GoToAsync("..");
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
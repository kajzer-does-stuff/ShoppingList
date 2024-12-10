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
        string shoppingListName = Regex.IsMatch(NewShoppingListName_Input.Text.ToString(), @".+") ?
                                    NewShoppingListName_Input.Text.ToString() : "Shopping List";

        string savePath = $"{FileSystem.AppDataDirectory}/{shoppingListName}.{Path.GetRandomFileName()}.shoppingList.txt";

        Models.ShoppingList newShoppingList = new Models.ShoppingList();
        newShoppingList.ListName = shoppingListName;
        newShoppingList.ListFileName = savePath;
        newShoppingList.ListID = new Random().Next();
        newShoppingList.ItemsList = new ObservableCollection<ShoppingListItem>();

        Models.ShoppingList.SaveShoppingList(savePath, newShoppingList);

        await Shell.Current.GoToAsync("..");
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
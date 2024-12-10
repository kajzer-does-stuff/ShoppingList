using ShoppingList.Models;
using System.Text.Json;
using System.Windows.Input;

namespace ShoppingList.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ShoppingListPage : ContentPage
{
    public ICommand CheckOutItemCommand => new Command(CheckOutItem_Clicked);
    public ICommand RemoveItemCommand => new Command(RemoveItem_Clicked);
    public ICommand IncreaseQtyCommand => new Command(IncreaseQty_Clicked);
    public ICommand DecreaseQtyCommand => new Command(DecreaseQty_Clicked);

    public string ItemId
    {
        set { LoadShoppingList(value); }
    }
	public ShoppingListPage()
	{
		InitializeComponent();

        LoadShoppingList($"{FileSystem.AppDataDirectory}/*.shoppinglist.txt");
	}

	public async void AddListItem_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new NewItemPage((Models.ShoppingList)BindingContext));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //TODO - actually implement how to get a list of all colview children
        foreach(CollectionView templatePage in ListOfItems.GetVisualTreeDescendants())
        {

        }

    }

    //item functions
    //TODO - put all the checkedOutItem/parentList shit in a separate function/class

    public void CheckOutItem_Clicked(object sender)
    {        
        if(sender != null)
        {
            ShoppingListItem checkedOutItem = (ShoppingListItem)(sender as ItemPage).BindingContext;
            Models.ShoppingList parentList = (Models.ShoppingList)(sender as ItemPage).Parent.BindingContext;            

            checkedOutItem.IsCheckedOut = !checkedOutItem.IsCheckedOut;
            SetUICheckOut(sender, checkedOutItem.IsCheckedOut);

            if (checkedOutItem.IsCheckedOut)
            {                
                parentList.ItemsList.Move(parentList.ItemsList.IndexOf(checkedOutItem), parentList.ItemsList.Count - 1);                
            }
            
            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }

    public void RemoveItem_Clicked(object sender)
    {
        if(sender != null)
        {
            ShoppingListItem checkedOutItem = (ShoppingListItem)(sender as ItemPage).BindingContext;
            Models.ShoppingList parentList = (Models.ShoppingList)(sender as ItemPage).Parent.BindingContext;

            parentList.ItemsList.Remove(checkedOutItem);
            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }

    public void IncreaseQty_Clicked(object sender)
    {
        if (sender != null)
        {
            ShoppingListItem checkedOutItem = (ShoppingListItem)(sender as ItemPage).BindingContext;
            Models.ShoppingList parentList = (Models.ShoppingList)(sender as ItemPage).Parent.BindingContext;

            checkedOutItem.ItemQuantity++;
            //TODO - this is painful, figure out how to update the ui some better way
            parentList.ItemsList[parentList.ItemsList.IndexOf(checkedOutItem)] = checkedOutItem;

            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }
    public void DecreaseQty_Clicked(object sender)
    {
        if (sender != null)
        {
            ShoppingListItem checkedOutItem = (ShoppingListItem)(sender as ItemPage).BindingContext;
            Models.ShoppingList parentList = (Models.ShoppingList)(sender as ItemPage).Parent.BindingContext;

            if(checkedOutItem.ItemQuantity > 0)
            {
                checkedOutItem.ItemQuantity--;
            }            
            parentList.ItemsList[parentList.ItemsList.IndexOf(checkedOutItem)] = checkedOutItem;
            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }

    public void SetUICheckOut(object senderPage, bool isCheckedOut)
    {
        if (senderPage != null)
        {
            Label itemNameDisplay = (Label)(senderPage as ItemPage).FindByName("Name");
            itemNameDisplay.TextDecorations = isCheckedOut ? TextDecorations.Strikethrough : TextDecorations.None;
        }
    }

    //loading the list
    private void LoadShoppingList(string _filePath)
    {
        if (File.Exists(_filePath))
        {
            Models.ShoppingList loadedList = new Models.ShoppingList();
            try
            {
                string loadedData = File.ReadAllText(_filePath);
                loadedList = JsonSerializer.Deserialize<Models.ShoppingList>(loadedData)!;
            }
            finally
            {
                BindingContext = loadedList;
            }            
        }
    }
}
using ShoppingList.Models;
using System.Collections.ObjectModel;
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

        LoadShoppingList($"{FileSystem.AppDataDirectory}/*.shoppinglist.json");
	}

	public async void AddListItem_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new NewItemPage((Models.ShoppingList)BindingContext));
    }

    public async void CheckOutItem_Clicked(object sender)
    {        
        if(sender != null)
        {
            ItemPage senderPage = sender as ItemPage;

            if(senderPage.Parent != null)
            {
                ShoppingListItem checkedOutItem = (ShoppingListItem)senderPage.BindingContext;
                Models.ShoppingList parentList = (Models.ShoppingList)senderPage.Parent.BindingContext;

                checkedOutItem.IsCheckedOut = !checkedOutItem.IsCheckedOut;

                if (checkedOutItem.IsCheckedOut)
                {
                    parentList.ItemsList.Move(parentList.ItemsList.IndexOf(checkedOutItem), parentList.ItemsList.Count - 1);
                }
                Models.ShoppingList.SaveShoppingList(parentList);          
            }
            
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
            //parentList.ItemsList[parentList.ItemsList.IndexOf(checkedOutItem)] = checkedOutItem;

            ((Editor)(sender as ItemPage).FindByName("ItemQty_Input")).Text = checkedOutItem.ItemQuantity.ToString();

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
            //parentList.ItemsList[parentList.ItemsList.IndexOf(checkedOutItem)] = checkedOutItem;

            ((Editor)(sender as ItemPage).FindByName("ItemQty_Input")).Text = checkedOutItem.ItemQuantity.ToString();

            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }
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
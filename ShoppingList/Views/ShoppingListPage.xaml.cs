using ShoppingList.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ShoppingList.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class ShoppingListPage : ContentPage
{
    public ICommand CheckOutItemCommand => new Command(CheckOutItem_Clicked);
    public ICommand RemoveItemCommand => new Command(RemoveItem_Clicked);
    public ICommand IncreaseQtyCommand => new Command(IncreaseQty_Clicked);
    public ICommand DecreaseQtyCommand => new Command(DecreaseQty_Clicked);
    public ICommand ItemQtyChangedCommand => new Command(ItemQty_Changed);

    private string _filePath = $"{FileSystem.AppDataDirectory}/*.shoppinglist.json";
    public string ItemId
    {
        set { LoadShoppingList(value); }
    }
	public ShoppingListPage()
	{
		InitializeComponent();

        LoadShoppingList(_filePath);
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

            if(checkedOutItem.ItemQuantity > 1)
            {
                checkedOutItem.ItemQuantity--;
            }            

            ((Editor)(sender as ItemPage).FindByName("ItemQty_Input")).Text = checkedOutItem.ItemQuantity.ToString();

            Models.ShoppingList.SaveShoppingList(parentList);
        }
    }
    public void ItemQty_Changed(object sender)
    {
        if (sender != null)
        {
            ShoppingListItem qtyChangedItem = (ShoppingListItem)(sender as ItemPage).BindingContext;

            if((sender as ItemPage).Parent != null)
            {
                Models.ShoppingList parentList = (Models.ShoppingList)(sender as ItemPage).Parent.BindingContext;

                string newValue = ((Editor)(sender as ItemPage).FindByName("ItemQty_Input")).Text;

                //walidacja musi byæ tu, inaczej nie zadzia³a poprawnie
                if (!String.IsNullOrWhiteSpace(newValue) && !String.IsNullOrEmpty(newValue))
                {
                    if (Regex.IsMatch(newValue.ToString(), @"^\d+"))
                    {
                       if(int.TryParse(newValue, out int val) && int.Parse(newValue) > 0)
                       {
                            qtyChangedItem.ItemQuantity = int.Parse(newValue);
                       }                              
                    }
                    else
                    {
                        ((Editor)(sender as ItemPage).FindByName("ItemQty_Input")).Text = qtyChangedItem.ItemQuantity.ToString();
                    }
                }
                Models.ShoppingList.SaveShoppingList(parentList);
            }
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
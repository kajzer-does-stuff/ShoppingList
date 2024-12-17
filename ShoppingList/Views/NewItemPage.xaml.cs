using ShoppingList.Models;
using System.Text.RegularExpressions;

namespace ShoppingList.Views;

public partial class NewItemPage : ContentPage
{
	public object parentList = null;

	public NewItemPage()
	{
		InitializeComponent();
	}
    public NewItemPage(object passedList)
    {
        InitializeComponent();
        parentList = (Models.ShoppingList)passedList;
    }
	private async void AddItem_Clicked(object sender, EventArgs e)
	{
        Models.ShoppingList castedParentList = (Models.ShoppingList)parentList;

		ShoppingListItem newItem = new ShoppingListItem();
		newItem.ItemName = UtilityThings.checkRegex(ItemName_Input.Text.ToString(), "Item", @".+");
		newItem.ItemQuantity = int.Parse(UtilityThings.checkRegex(ItemQuantity_Input.Text.ToString(), "0", @"^\d+"));
		newItem.ItemQuantityType = UtilityThings.checkRegex(ItemQtyType_Input.SelectedItem.ToString(), "Thing", @".+");
		newItem.IsCheckedOut = false;
        newItem.ItemIdInList = castedParentList.ItemsList.Count;

        castedParentList.ItemsList.Add(newItem);

		Models.ShoppingList.SaveShoppingList(castedParentList);

        await Shell.Current.GoToAsync("..");
    }
	private async void Cancel_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }	
}
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

		ShoppingListItem newItem = new ShoppingListItem()
		{
			ItemName = "Przedmiot",
			ItemQuantity = 1,
			ItemQuantityType = "szt.",
			ItemIdInList = castedParentList.ItemsList.Count,
			IsCheckedOut = false
		};

		if (ItemName_Input.Text != null && Regex.IsMatch(ItemName_Input.Text.ToString(), @".+"))
			newItem.ItemName = ItemName_Input.Text.ToString();

		if (ItemQuantity_Input.Text != null && UtilityThings.ValidateQuantity(ItemQuantity_Input.Text.ToString()))
            newItem.ItemQuantity = int.Parse(ItemQuantity_Input.Text.ToString());			

		if (ItemQtyType_Input.SelectedItem != null && ItemQtyType_Input.SelectedItem.ToString() != null)		
			newItem.ItemQuantityType = ItemQtyType_Input.SelectedItem.ToString();

        castedParentList.ItemsList.Add(newItem);

		Models.ShoppingList.SaveShoppingList(castedParentList);

        await Shell.Current.GoToAsync("..");
    }
	private async void Cancel_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }	
}
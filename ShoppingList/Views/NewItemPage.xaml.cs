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
		//explicit parentList cast to ShoppingList, so that I don't have to write ((Models.ShoppingList)parentList) every time
        Models.ShoppingList castedParentList = (Models.ShoppingList)parentList;

		//TODO - put the regex in a global utility class
        string itemName = checkRegex(ItemName_Input.Text.ToString(), "Item", @".+");
        int itemQuantity = int.Parse(checkRegex(ItemQuantity_Input.Text.ToString(), "0", @"\d+"));
        string itemQuantityType = checkRegex(ItemQuantityType_Input.Text.ToString(), "Thing", @".+");
        string itemCategory = checkRegex(ItemCategory_Input.Text.ToString(), "Other", @".+");

		ShoppingListItem newItem = new ShoppingListItem();		
		
		//maybe simplify this w/above?
		newItem.ItemName = itemName;
		newItem.ItemQuantity = itemQuantity;
		newItem.ItemQuantityType = itemQuantityType;
		newItem.ItemCategory = itemCategory;
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
	private string checkRegex(string trueString, string falseString, string pattern)
	{
		if (Regex.IsMatch(trueString, pattern))
		{
			return trueString;
		}
		else
		{
			return falseString;
		}
	}
}
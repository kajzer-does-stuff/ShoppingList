using ShoppingList.Models;
using System.Windows.Input;

namespace ShoppingList.Views;

public partial class ItemPage : ContentView
{
    public static readonly BindableProperty ItemNameProperty = BindableProperty.Create(nameof(ItemName), typeof(string), typeof(ItemPage), string.Empty);
    public static readonly BindableProperty ItemQuantityProperty = BindableProperty.Create(nameof(ItemQuantity), typeof(int), typeof(ItemPage), 0);
    public static readonly BindableProperty ItemQtyTypeProperty = BindableProperty.Create(nameof(ItemQtyType), typeof(string), typeof(ItemPage), string.Empty);
    public static readonly BindableProperty IsCheckedOutProperty = BindableProperty.Create(nameof(IsCheckedOut), typeof(bool), typeof(ItemPage), false);

    public static readonly BindableProperty CheckOutItemClickedProperty = BindableProperty.Create(nameof(CheckOutItemClicked), typeof(ICommand), typeof(ItemPage));
    public static readonly BindableProperty RemoveItemClickedProperty = BindableProperty.Create(nameof(RemoveItemClicked), typeof(ICommand), typeof(ItemPage));
    public static readonly BindableProperty IncreaseQtyClickedProperty = BindableProperty.Create(nameof(IncreaseQtyClicked), typeof(ICommand), typeof(ItemPage));
    public static readonly BindableProperty DecreaseQtyClickedProperty = BindableProperty.Create(nameof(DecreaseQtyClicked), typeof(ICommand), typeof(ItemPage));

    public string ItemName
	{
		get => (string)GetValue(ItemNameProperty);
        set => SetValue(ItemNameProperty, value);
	}
    public int ItemQuantity
    {
        get => (int)GetValue(ItemQuantityProperty);
        set => SetValue(ItemQuantityProperty, value);
    }
    public string ItemQtyType
    {
        get => (string)GetValue(ItemQtyTypeProperty);
        set => SetValue(ItemQtyTypeProperty, value);
    }
    public bool IsCheckedOut
    {
        get => (bool)GetValue(IsCheckedOutProperty);
        set => SetValue(IsCheckedOutProperty, value);
    }
    public ICommand CheckOutItemClicked
    {
        get => (ICommand)GetValue(CheckOutItemClickedProperty);
        set => SetValue(CheckOutItemClickedProperty, value);
    }
    public ICommand RemoveItemClicked
    {
        get => (ICommand)GetValue(RemoveItemClickedProperty);
        set => SetValue(RemoveItemClickedProperty, value);
    }
    public ICommand IncreaseQtyClicked
    {
        get => (ICommand)GetValue(IncreaseQtyClickedProperty);
        set => SetValue(IncreaseQtyClickedProperty, value);
    }
    public ICommand DecreaseQtyClicked
    {
        get => (ICommand)GetValue(DecreaseQtyClickedProperty);
        set => SetValue(DecreaseQtyClickedProperty, value);
    }
    public ItemPage()
    {
        InitializeComponent();
        BindingContext = this;
        System.Diagnostics.Debug.WriteLine(BindingContext);
    }
}
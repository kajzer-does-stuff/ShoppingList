namespace ShoppingList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.ShoppingListPage), typeof(Views.ShoppingListPage));
            Routing.RegisterRoute(nameof(Views.NewItemPage), typeof(Views.NewItemPage));
            Routing.RegisterRoute(nameof(Views.AllShoppingListsPage), typeof(Views.AllShoppingListsPage));
            Routing.RegisterRoute(nameof(Views.NewShoppingListPage), typeof(Views.NewShoppingListPage));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    internal class AllShoppingLists
    {
        public ObservableCollection<ShoppingList> ShoppingListCollection { get; set; } = new ObservableCollection<ShoppingList>();
        public AllShoppingLists() => LoadAllShoppingLists();

        public void LoadAllShoppingLists()
        {
            ShoppingListCollection.Clear();

            string _SavePath = FileSystem.AppDataDirectory;

            IEnumerable<ShoppingList> ShoppingLists = Directory
                .EnumerateFiles(_SavePath, "*.shoppingList.txt")
                .Select(file =>
                {
                    try
                    {
                        ShoppingList tempList = JsonSerializer.Deserialize<ShoppingList>(File.ReadAllText(file)!);
                        return tempList != null ? tempList : new ShoppingList();
                    }
                    catch (JsonException e)
                    {
                        return new ShoppingList();
                    }
                })
                .OrderBy(list => list.ListName);

            foreach (ShoppingList _list in ShoppingLists)
            { 
                ShoppingListCollection.Add(_list);
            }
        }
    }
}

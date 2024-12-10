using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ShoppingList.Models;

namespace ShoppingList.Models
{
    internal class ShoppingList
    {
        public ObservableCollection<ShoppingListItem> ItemsList { get; set; } = new ObservableCollection<ShoppingListItem>();
        public int ListID { get; set; } = 0;
        public string ListName { get; set; } = "Shopping List";
        public string ListFileName { get; set; } = $"{FileSystem.AppDataDirectory}/test_shopping_list.shoppingList.txt";
        public static void SaveShoppingList(string savePath, ShoppingList saveList)
        {
            string saveData = JsonSerializer.Serialize(saveList);
            File.WriteAllText(savePath, saveData);
        }
        public static void SaveShoppingList(ShoppingList saveList)
        {
            string saveData = JsonSerializer.Serialize(saveList);
            File.WriteAllText(saveList.ListFileName, saveData);
        }
    }
}

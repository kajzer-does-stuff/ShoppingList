﻿using System;
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
        public ObservableCollection<ShoppingListItem> ItemsList { get; set; } = new();
        public int ListID { get; set; } = 0;
        public string ListName { get; set; } = "Lista";
        public string ListFileName { get; set; } = $"{FileSystem.AppDataDirectory}/lista.shoppingList.json";
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

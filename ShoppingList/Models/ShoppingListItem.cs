using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    internal class ShoppingListItem
    {
        public int ItemIdInList { get; set; }
        public string ItemName { get; set; } = "List Item";
        public int ItemQuantity { get; set; }
        public string ItemQuantityType { get; set; } = "";
        // public ShoppingListCategory ItemCategory { get; set; } = new();
        public string ItemCategory { get; set; } = "Other";
        public bool IsCheckedOut { get; set; } = false;
    }
}

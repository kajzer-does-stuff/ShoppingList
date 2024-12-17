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
        public string ItemName { get; set; } = "Produkt";
        public int ItemQuantity { get; set; }
        public string ItemQuantityType { get; set; } = "szt.";
        public bool IsCheckedOut { get; set; } = false;
    }
}

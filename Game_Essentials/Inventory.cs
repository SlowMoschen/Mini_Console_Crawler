using System;
using System.Collections.Generic;
using System.Linq;
using _Items;

/**
*
*   Inventory
*   - Used by the player to store items
*
*   @param List<Item> items - The list of items in the inventory
*   @param int gold - The amount of gold the player has
*
*/

namespace _Inventory 
{
    public class InventoryManager {
        public List<Item> items { get; set; }
        public int gold { get; set; }

        public InventoryManager() {
            this.items = new List<Item>();
            this.gold = 0;
        }

        public Item getExistingItem(string itemType) {
            return this.items.FirstOrDefault(item => item.type == itemType);
        }

        public void addItem(Item item) {
            Item existingItem = getExistingItem(item.type);
            if(existingItem != null) {
                existingItem.quantity += 1;
            } else {
                this.items.Add(item);
            }
        }

        public void removeItem(Item item) {
            Item existingItem = getExistingItem(item.type);
            if(existingItem != null) {
                existingItem.quantity -= 1;
                if(existingItem.quantity <= 0) {
                    this.items.Remove(existingItem);
                }
            }
        }

        public int getItemQuantity(string itemName) {
            Item existingItem = getExistingItem(itemName);
            if(existingItem != null) {
                return existingItem.quantity;
            } else {
                return 0;
            }
        }
    }
}
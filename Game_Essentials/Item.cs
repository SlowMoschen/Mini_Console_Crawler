using Game_Characters;

namespace _Items 
{
    public class Item {
        public string name { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; } = 1;
        public int maxQuantity { get; set; }

        public Item(string name, string type, string description, int goldValue, int maxQuantity) {
            this.name = name;
            this.type = type;
            this.description = description;
            this.price = goldValue;
            this.maxQuantity = maxQuantity;
        }
    }

    public class Potion : Item {
        public int effectValue { get; set; }

        public Potion(string name, string type, string description, int price, int maxQuantity, int effectValue) : base(name, type, description, price, maxQuantity) {
            this.effectValue = effectValue;
        }

        public void usePotion(Player target) {
            switch (this.type) {
                case "Health Potion":
                    target.health += this.effectValue;
                    break;
                case "Strength Potion":
                    target.strength += this.effectValue;
                    break;
                case "Endurance Potion":
                    target.endurance += this.effectValue;
                    break;
            }
        }
    }
}
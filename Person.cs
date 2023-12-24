namespace Person
{

    public class Person {

        public string name;
        public int attack;
        public double strength;
        public int armor;
        public double health;

        public bool isDefending = false;
        
        // Count the number of options for the user - expamlpe: Attack, Defend - is used to get Random Choice for enemy
        public static int optionsCount = 2;

        public Person (string name, int attack, double strength, int armor, double health) {
            this.name = name;
            this.attack = attack;
            this.strength = strength;
            this.armor = armor;
            this.health = health;
        }

        public void Attack (Person target) {
            if(target.isDefending) {
                // target.health -= this.attack * this.strength / target.armor / 2;
                // Console.WriteLine(" The damage was blocked");
                target.isDefending = false;
            } else {
                target.health -= this.attack * this.strength / target.armor;
            }
        }

        // Function to defend next attack
        public void Defend (Person target) {
            this.isDefending = true;
        }

        // Print Battle Stats
        public void printBattleStats () {
            Console.WriteLine(" Name: " + this.name);
            Console.WriteLine(" Health: " + this.health);
            Console.WriteLine(" Armor: " + this.armor);
        }

        public void printStats () {
            Console.WriteLine(" Name: " + this.name);
            Console.WriteLine(" Attack: " + this.attack);
            Console.WriteLine(" Strength: " + this.strength);
            Console.WriteLine(" Armor: " + this.armor);
            Console.WriteLine(" Health: " + this.health);
        }

    }
}
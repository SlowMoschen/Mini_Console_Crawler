namespace Enemy
{
    public class Enemy : Person.Person
    {

        public int experienceOnDefeat;
        public static int attackCount;

        public Enemy(string name, int attack, double strength, int armor, double health, int experienceOnDefeat)
            : base(name, attack, strength, armor, health)
        {
            this.experienceOnDefeat = experienceOnDefeat;
        }
    
    // fucntion to get a random choice for the enemy
        public static int getRandomAttack() {
            Random random = new Random();
            int choice = random.Next(1, attackCount + optionsCount);
            return choice;
        }

        public new void printBattleStats () {
            Console.WriteLine(" Enemy: " + this.name);
            Console.WriteLine(" Health: " + this.health);
            Console.WriteLine(" Armor: " + this.armor);
        }
    }


}
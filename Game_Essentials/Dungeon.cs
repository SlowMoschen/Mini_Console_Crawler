using Zombie;
using Enemy;

namespace Dungeon {

    public class ZombieStats {
        public static int attack;
        public static double strength;
        public static int armor;
        public static double health;
        public static int experienceOnDefeat;

        public ZombieStats() {
            attack = 10;
            strength = 1.0;
            armor = 5;
            health = 50;
            experienceOnDefeat = 20;
        }
    }

    public class RoomGenerator {

        ZombieStats ZombieStats = new ZombieStats();
        static int easyEnemeies = 1;
        static int mediumEnemeies = 3;
        static int hardEnemeies = 5;

        public static Zombie.Zombie[] generateRoom(string difficulty) {
            // Generate a random room based on the difficulty
            // Easy - 1 enemy
            // Medium - 3 enemies
            // Hard - 5 enemies
            switch (difficulty)
            {
                case "Easy":
                    return generateZombies(easyEnemeies);
                case "Medium":
                    return generateZombies(mediumEnemeies);
                case "Hard":
                    return generateZombies(hardEnemeies);
            }

            return generateZombies(1);
        }

        // Generate a number of zombies
        public static Zombie.Zombie[] generateZombies(int count) {
            Zombie.Zombie[] zombies = new Zombie.Zombie[count];

            for (int i = 0; i < count; i++)
            {
                zombies[i] = new Zombie.Zombie("Zombie", attack: ZombieStats.attack, strength: ZombieStats.strength, armor: ZombieStats.armor, health: ZombieStats.health, experienceOnDefeat: ZombieStats.experienceOnDefeat);
            }            

            return zombies;
        }
    }
}
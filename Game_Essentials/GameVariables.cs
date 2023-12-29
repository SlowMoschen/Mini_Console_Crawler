using UserInput;
using Console_RPG;
using Game_Characters;
using Dungeon_Generator;

namespace Game_Essentials {

    public class GameVariables {

    // Method to get a chance for something
    // Example: getChance(50) - will return true 50% of the time
    public static bool getChance(int chance) {
        Random random = new Random();
        int randomNumber = random.Next(1, 100);
        if(randomNumber <= chance) {
            return true;
        } else {
            return false;
        }
    }
        public class GameStats {
            public static string version { get; } = "0.6.3";
            public static int surviedRooms { get; set;} = 0;
            public static int survivedDungeons { get; set;} = 0;
            public static int killedEnemies { get; set;} = 0;
            public static int totalExperience { get; set;} = 0;
            public static int totalGold { get; set;} = 0;
        }

        public class WeaponStats {

            public class Sword {
                public static int attack { get; } = 12;
                public static int enduranceCost { get; } = 10;
                public static int specialAttackEnduranceCost { get; } = 20;
                public static int specialAttackStrength { get; } = 20;
            }

            public class Axe {
                public static int attack { get; } = 25;
                public static int enduranceCost { get; } = 20;
                public static int specialAttackEnduranceCost { get; } = 30;
                public static int specialAttackStrength { get; } = 30;
            }

            public class Mace {
                public static int attack { get; } = 20;
                public static int enduranceCost { get; } = 15;
                public static int specialAttackEnduranceCost { get; } = 25;
                public static int specialAttackStrength { get; } = 23;
            }
        }

        public class EnemyStats {

            public class Zombie {
                public static int attack { get; } = 15;
                public static double strength { get; } = 1.0;
                public static int armor { get; } = 5;
                public static double health { get; } = 50;
                public static int experienceOnDefeat { get; } = 15;
                public static int goldOnDefeat { get; } = 5;
            }

            public class Spider {

            public static int attack { get; } = 20;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 2;
            public static double health { get; } = 80;
            public static int experienceOnDefeat { get; } = 30;
            public static int goldOnDefeat { get; } = 10;
            public static int poisonDamage { get; } = 5;
            public static int poisonChance { get; } = 25;
        }

        public class Goblin {
            public static int attack { get; } = 5;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 10;
            public static double health { get; } = 40;
            public static int experienceOnDefeat { get; } = 20;
            public static int goldOnDefeat { get; } = 15;
            public static int stealAmount { get; } = 3;
        }

        public class Assassin {
            public static int attack { get; } = 50;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 0;
            public static double health { get; } = 30;
            public static int experienceOnDefeat { get; } = 15;
            public static int goldOnDefeat { get; } = 5;
        }

        public class StoneGolem {
            public static int attack { get; } = 10;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 20;
            public static double health { get; } = 100;
            public static int experienceOnDefeat { get; } = 50;
            public static int goldOnDefeat { get; } = 20;
            public static int stunChance { get; } = 15;
        }
    }


        public class GameSettings {
            
            public static string playerName { get; set;} = "Player";
            public static int healPotionHealRating { get; } = 20;
            public static int strengthPotionStrengthRating { get; } = 2;
            public static int endurancePotionEnduranceRating { get; } = 50;
            public static int enduranceRegeneration { get; } = 7;

            public class ItemMaxQuantity {
                public static int healPotionMaxQuantity { get; } = 5;
                public static int strengthPotionMaxQuantity { get; } = 3;
                public static int endurancePotionMaxQuantity { get; } = 5;
            }
            public class ItemPrices {
                public static int healPotionPrice { get; } = 15;
                public static int strengthPotionPrice { get; } = 35;
                public static int endurancePotionPrice { get; } = 25;
            }
            
            public class EffectDurations {
                public static int poisonDuration { get; } = 3;
                public static int strengthDuration { get; } = 7;
                public static int stunDuration { get; } = 1;
            }
            
            public class Options {
                public static string[] difficultyOptions { get; } = new string[] { "Easy", "Medium", "Hard" };
                public static string[] battleOptions { get; } = new string[] { "Attack", "Rest", "Use Potion", "Defend", "Run" };
                public static string[]? attackOptions { get; set; }
                public static string[] mainMenuOptions { get; } = new string[] { "Enter Dungeon", "View Stats", "View Inventory", "Shop" };
                public static string[] shopMenuOptions { get; } = new string[] { "Buy", "Exit" }; 
                public static string[] shopItems { get; } = new string[] { "Heal Potion", "Strength Potion", "Endurance Potion" };
            }

        public class DungeonSettings {
                private static Random random = new Random();
                public static string[] enemyTypes { get; } = new string[] { "Zombie", "Spider", "Goblin", "Assassin", "Stone Golem" };
                public static int easyRooms { get; } = 1;
                public static int mediumRooms { get; } = 2;
                public static int hardRooms { get; } = 3;
                
                /**
                * Generate a random number of mobs based on the difficulty
                * 1 - 3 easy mobs
                * 3 - 5 medium mobs
                * 5 - 10 hard mobs
                */
                public static int easyMobs => random.Next(1, 4);
                public static int mediumMobs => random.Next(3, 6);
                public static int hardMobs => random.Next(5, 11);
            }
        }

        public class GameLoopBooleans {
            public static bool ranAway { get; set;} = false;
            public static bool isInMenu { get; set;} = true;
            public static bool isInShop { get; set;} = false;
            public static bool isInFight { get; set;} = false;
            public static bool isDead { get; set;} = false;
            public static bool isDungeonCleared { get; set;} = false;
            public static bool wasPlayerAttackMade { get; set;} = false;
            public static bool wasEnemyAttackMade { get; set;} = false;
        }

    }
}
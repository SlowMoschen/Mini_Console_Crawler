using UserInput;
using Console_RPG;
using Game_Characters;
using Dungeon_Generator;

/**
* This class contains all the variables that are used in the game
* It is divided into sections:
* - GameStats - contains general stats of the Game
* - WeaponStats - contains stats of the Weapons
* - LevelUpRatings - contains ratings for the level up system
* - PlayerStats - contains stats of the Player
* - EnemyStats - contains stats of the Enemies
* - GameSettings - contains settings of the Game
* - GameLoopBooleans - contains booleans that are used in the GameLoop
*/

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
            public static string version { get; } = "0.8.7";
            public static int surviedRooms { get; set;} = 0;
            public static int survivedDungeons { get; set;} = 0;
            public static int killedEnemies { get; set;} = 0;
            public static int totalExperience { get; set;} = 0;
            public static int totalGold { get; set;} = 0;
        }

        public class WeaponStats {
            private static Random random = new Random();

            public int baseMinAttack { get; } 
            public int baseMaxAttack { get; }
            public int baseMinSpecialAttack { get; }
            public int baseMaxSpecialAttack { get; }
            public int minMinMuliplier { get; }
            public int minMaxMuliplier { get; }
            public int enduranceCost { get; }
            public int specialAttackEnduranceCost { get; }
            public string specialAttackName { get; }

            public WeaponStats(int baseMinAttack, int baseMaxAttack, int baseMinSpecialAttack, int baseMaxSpecialAttack, int minMinMuliplier, int minMaxMuliplier, int enduranceCost, int specialAttackEnduranceCost, string specialAttackName) {
                this.baseMinAttack = baseMinAttack;
                this.baseMaxAttack = baseMaxAttack;
                this.baseMinSpecialAttack = baseMinSpecialAttack;
                this.baseMaxSpecialAttack = baseMaxSpecialAttack;
                this.minMinMuliplier = minMinMuliplier;
                this.minMaxMuliplier = minMaxMuliplier;
                this.enduranceCost = enduranceCost;
                this.specialAttackEnduranceCost = specialAttackEnduranceCost;
                this.specialAttackName = specialAttackName;
            }

            public int getAttack() {
                return CalculateAttack(baseMinAttack, baseMaxAttack, minMinMuliplier, minMaxMuliplier);
            }

            public int getSpecialAttackStrength() {
                return CalculateAttack(baseMinSpecialAttack, baseMaxSpecialAttack, minMinMuliplier, minMaxMuliplier);
            }

            private static int CalculateAttack(int baseMinAttack, int baseMaxAttack, int minMinMuliplier, int minMaxMuliplier) {
                Random random = new Random();
                int playerLevel = GameVariables.PlayerStats.level;
                int min = baseMinAttack + ((playerLevel - 1) / GameVariables.GameSettings.weaponUpgradeInterval) * minMinMuliplier;
                int max = baseMaxAttack + ((playerLevel - 1) / GameVariables.GameSettings.weaponUpgradeInterval) * minMaxMuliplier;

                return random.Next(min, max + 1);
            }

            public static WeaponStats Sword = new WeaponStats(
                baseMinAttack: 20,
                baseMaxAttack: 28,
                baseMinSpecialAttack: 28,
                baseMaxSpecialAttack: 35,
                minMinMuliplier: 10,
                minMaxMuliplier: 15,
                enduranceCost: 10,
                specialAttackEnduranceCost: 18,
                specialAttackName: "Slash"
            );
            public static WeaponStats Axe = new WeaponStats(
                baseMinAttack: 25,
                baseMaxAttack: 35,
                baseMinSpecialAttack: 35,
                baseMaxSpecialAttack: 45,
                minMinMuliplier: 12,
                minMaxMuliplier: 17,
                enduranceCost: 18,
                specialAttackEnduranceCost: 28,
                specialAttackName: "Chop"
            );
            public static WeaponStats Mace = new WeaponStats(
                baseMinAttack: 22,
                baseMaxAttack: 30,
                baseMinSpecialAttack: 30,
                baseMaxSpecialAttack: 40,
                minMinMuliplier: 12,
                minMaxMuliplier: 18,
                enduranceCost: 12,
                specialAttackEnduranceCost: 20,
                specialAttackName: "Bash"
            );

            
        }

        public class LevelUpRatings {
            public static int increaseAttackRating { get; } = 10;
            public static int increaseArmorRating { get; } = 2;
            public static int increaseMaxHealthRating{ get; } = 100;
            public static int experienceRating { get; } = 100;
        }

        public class PlayerStats {
            public static string playerName { get; set;} = "Player";
            public static int attack { get; set; } = 50;
            public static double strength { get; set; } = 1.0;
            public static int armor { get; set; } = 50;
            public static double health { get; set; } = 1000;
            public static int maxHealth { get; set; } = 1000;
            public static int experience { get; set;} = 0;
            public static int experienceToLevelUp { get; set;} = 100;
            public static int level { get; set;} = 1;
            public static int maxLevel { get; } = 30;
        }

        public class EnemyStats {

            public class Zombie {
                public static int attack { get; } = 15;
                public static double strength { get; } = 1.0;
                public static int armor { get; } = 5;
                public static double health { get; } = 50;
                public static int experienceOnDefeat { get; } = 15;
                public static int goldOnDefeat { get; } = 5;
                public static string[] attackNames { get; } = new string[] { "Bite", "Thrash" };
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
            public static string[] attackNames { get; } = new string[] { "Spit" };
        }

        public class Goblin {
            public static int attack { get; } = 5;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 10;
            public static double health { get; } = 40;
            public static int experienceOnDefeat { get; } = 20;
            public static int goldOnDefeat { get; } = 15;
            public static int stealAmount { get; } = 3;
            public static string[] attackNames { get; } = new string[] { "Steal" };
        }

        public class Assassin {
            public static int attack { get; } = 50;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 0;
            public static double health { get; } = 30;
            public static int experienceOnDefeat { get; } = 15;
            public static int goldOnDefeat { get; } = 5;
            public static string[] attackNames { get; } = new string[] { "Backstab" };
        }

        public class StoneGolem {
            public static int attack { get; } = 10;
            public static double strength { get; } = 1.0;
            public static int armor { get; } = 20;
            public static double health { get; } = 100;
            public static int experienceOnDefeat { get; } = 50;
            public static int goldOnDefeat { get; } = 20;
            public static int stunChance { get; } = 15;
            public static string[] attackNames { get; } = new string[] { "Slam" };
        }

        public class Dragon {
            public static int attack { get; } = 50;
            public static double strength { get; } = 1.5;
            public static int armor { get; } = 35;
            public static double health { get; } = 250;
            public static int experienceOnDefeat { get; } = 1200;
            public static int goldOnDefeat { get; } = 350;
            public static int fireBreathDamage { get; } = 10;
            public static int burnChance { get; } = 33;
            public static int burningDamage { get; } = 12;
            public static int throwRockDamage { get; } = 20;
            public static int stunChance { get; } = 25;
            public static int tailStrikeDamage { get; } = 30;
            public static string[] attackNames { get; } = new string[] { "Fire Breath", "Rock Throw", "Tail Strike" };
        }
    }


        public class GameSettings {            
            public static int healPotionHealRating { get; } = 20;
            public static int strengthPotionStrengthRating { get; } = 2;
            public static int endurancePotionEnduranceRating { get; } = 50;
            public static int enduranceRegeneration { get; } = 7;
            
            // Every 3 levels the Weapons get stronger
            public static int weaponUpgradeInterval { get; } = 3;

            public class ItemMaxQuantity {
                public static int healPotionMaxQuantity { get; } = 5;
                public static int strengthPotionMaxQuantity { get; } = 3;
                public static int endurancePotionMaxQuantity { get; } = 5;
            }
            public class ItemPrices {
                public static int healPotionPrice { get; } = 15;
                public static int strengthPotionPrice { get; } = 35;
                public static int endurancePotionPrice { get; } = 25;
                public static string[] allItemPrices { get; } = new string[] { healPotionPrice.ToString(), strengthPotionPrice.ToString(), endurancePotionPrice.ToString() };
            }
            
            public class EffectDurations {
                public static int poisonDuration { get; } = 3;
                public static int burnDuration { get; } = 3;
                public static int strengthDuration { get; } = 7;
                public static int stunDuration { get; } = 1;
            }
            
            public class Options {
                public static string[] difficultyOptions { get; } = new string[] { "Easy", "Medium", "Hard", "Boss", "Dev" };
                public static string[] battleOptions { get; } = new string[] { "Attack", "Rest", "Use Potion", "Defend", "Run" };
                public static string[]? attackOptions { get; set; }
                public static string[] mainMenuOptions { get; } = new string[] { "Enter Dungeon", "View Stats", "View Inventory", "Shop", "Game Infos" };
                public static string[] shopMenuOptions { get; } = new string[] { "Buy", "Exit" }; 
                public static string[] shopItems { get; } = new string[] { "Heal Potion", "Strength Potion", "Endurance Potion" };
            }

        public class DungeonSettings {
                private static Random random = new Random();
                public static string[] enemyTypes { get; } = new string[] { "Zombie", "Spider", "Goblin", "Assassin", "Stone Golem" };
                public static int easyRooms { get; } = 1;
                public static int mediumRooms { get; } = 2;
                public static int hardRooms { get; } = 3;
                public static int bossRooms { get; } = 5;
                
                /**
                * Generate a random number of mobs based on the difficulty
                * 1 - 3 easy mobs
                * 3 - 5 medium mobs
                * 5 - 10 hard mobs
                */
                public static int easyMobs => random.Next(1, 4);
                public static int mediumMobs => random.Next(3, 6);
                public static int hardMobs => random.Next(5, 11);
                public static int bossMobs => random.Next(10, 21);
                public static int easyGold => random.Next(5, 11);
                public static int mediumGold => random.Next(10, 21);
                public static int hardGold => random.Next(20, 31);
                public static int bossGold => random.Next(40, 81);
                public static int getChestItemsCount(string difficulty) {
                    switch (difficulty)
                    {
                        // Easy - 1 item
                        case "Easy":
                            return 1;
                        // Medium - 2 - 3 items
                        case "Medium":
                            return random.Next(2, 4);
                        // Hard - 3 - 5 items
                        case "Hard":
                            return random.Next(3, 5);
                        case "Boss":
                            return 5;
                        default:
                            return 0;
                    }
                }
                public static string[] chestItems { get; } = new string[] { "Heal Potion", "Strength Potion", "Endurance Potion" };
                public static string[] chestWeapons { get; } = new string[] { "Sword", "Axe", "Mace" };
            }
        }

        public class GameLoopBooleans {
            public static bool ranAway { get; set;} = false;
            public static bool isInTutorial { get; set;} = false;
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
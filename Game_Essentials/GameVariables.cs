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
            public static string version { get; } = "0.9.2";
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

            public int attack { get; }
            public double strength { get; }
            public int armor { get; }
            public double health { get; }
            public int experienceOnDefeat { get; }
            public int goldOnDefeat { get; }
            public string[] attackNames { get; }

            public EnemyStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, string[] attackNames) {
                this.attack = attack;
                this.strength = strength;
                this.armor = armor;
                this.health = health;
                this.experienceOnDefeat = experienceOnDefeat;
                this.goldOnDefeat = goldOnDefeat;
                this.attackNames = attackNames;
            }
            
            public class SpiderStats : EnemyStats {
                public int poisonDamage { get; }
                public int poisonChance { get; }
                
                public SpiderStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int poisonDamage, int poisonChance, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, attackNames) {
                    this.poisonDamage = poisonDamage;
                    this.poisonChance = poisonChance;
                }
            }

            public class GiantSpiderStats : SpiderStats {
                public int stunChance { get; }
                public int webShotDamage { get; }
                public int poisonBiteDamage { get; }

                public GiantSpiderStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int poisonDamage, int poisonChance, int stunChance, int webShotDamage, int poisonBiteDamage, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, poisonDamage, poisonChance, attackNames) {
                    this.stunChance = stunChance;
                    this.webShotDamage = webShotDamage;
                    this.poisonBiteDamage = poisonBiteDamage;
                }
            }

            public class GoblinStats : EnemyStats {
                public int stealAmount { get; }
                
                public GoblinStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int stealAmount, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, attackNames) {
                    this.stealAmount = stealAmount;
                }
            }

            public class StoneGolemStats : EnemyStats {
                public int stunChance { get; }
                
                public StoneGolemStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int stunChance, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, attackNames) {
                    this.stunChance = stunChance;
                }
            }

            public class DemonicSorcererStats : EnemyStats {
                public int hellFireBlastDamage { get; }
                public int burnChance { get; }
                public int burningDamage { get; }
                public double darkPactAttackPercentage { get; }
                public double darkPactHealthPercentage { get; }
                
                public DemonicSorcererStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int hellFireBlastDamage, int burnChance, int burningDamage, double darkPactAttackPercentage, double darkPactHealthPercentage, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, attackNames) {
                    this.hellFireBlastDamage = hellFireBlastDamage;
                    this.burnChance = burnChance;
                    this.burningDamage = burningDamage;
                    this.darkPactAttackPercentage = darkPactAttackPercentage;
                    this.darkPactHealthPercentage = darkPactHealthPercentage;
                }
            }

            public class DragonStats : EnemyStats {
                public int fireBreathDamage { get; }
                public int burnChance { get; }
                public int burningDamage { get; }
                public int throwRockDamage { get; }
                public int stunChance { get; }
                public int tailStrikeDamage { get; }
                
                public DragonStats(int attack, double strength, int armor, double health, int experienceOnDefeat, int goldOnDefeat, int fireBreathDamage, int burnChance, int burningDamage, int throwRockDamage, int stunChance, int tailStrikeDamage, string[] attackNames) : base(attack, strength, armor, health, experienceOnDefeat, goldOnDefeat, attackNames) {
                    this.fireBreathDamage = fireBreathDamage;
                    this.burnChance = burnChance;
                    this.burningDamage = burningDamage;
                    this.throwRockDamage = throwRockDamage;
                    this.stunChance = stunChance;
                    this.tailStrikeDamage = tailStrikeDamage;
                }
            }

            public static EnemyStats Zombie = new EnemyStats(
                attack: 15,
                strength: 1.0,
                armor: 5,
                health: 50,
                experienceOnDefeat: 15,
                goldOnDefeat: 5,
                attackNames: new string[] { "Bite", "Thrash" }
            );

            public static SpiderStats Spider = new SpiderStats(
                attack: 20,
                strength: 1.0,
                armor: 2,
                health: 80,
                experienceOnDefeat: 30,
                goldOnDefeat: 10,
                attackNames: new string[] { "Spit" },
                poisonDamage: 5,
                poisonChance: 25
            );

            public static GoblinStats Goblin = new GoblinStats(
                attack: 5,
                strength: 1.0,
                armor: 10,
                health: 40,
                experienceOnDefeat: 20,
                goldOnDefeat: 15,
                attackNames: new string[] { "Steal" },
                stealAmount: 3
            );

            public static EnemyStats Assassin = new EnemyStats(
                attack: 50,
                strength: 1.0,
                armor: 0,
                health: 30,
                experienceOnDefeat: 15,
                goldOnDefeat: 5,
                attackNames: new string[] { "Backstab" }
            );

            public static StoneGolemStats StoneGolem = new StoneGolemStats(
                attack: 10,
                strength: 1.0,
                armor: 20,
                health: 100,
                experienceOnDefeat: 50,
                goldOnDefeat: 20,
                attackNames: new string[] { "Slam" },
                stunChance: 15
            );

            /**
            *
            *   Mini Bosses
            *
            */

            public static GiantSpiderStats GiantSpider = new GiantSpiderStats(
                attack: 30,
                strength: 1.0,
                armor: 10,
                health: 150,
                experienceOnDefeat: 100,
                goldOnDefeat: 50,
                attackNames: new string[] { "Web Shot", "Poison Bite" },
                poisonDamage: 10,
                poisonChance: 15,
                stunChance: 25,
                webShotDamage: 20,
                poisonBiteDamage: 30
            );

            public static DemonicSorcererStats DemonicSorcerer = new DemonicSorcererStats(
                attack: 30,
                strength: 1.0,
                armor: 5,
                health: 150,
                experienceOnDefeat: 100,
                goldOnDefeat: 50,
                attackNames: new string[] { "Hell Fire Blast", "Dark Pact" },
                hellFireBlastDamage: 10,
                burnChance: 15,
                burningDamage: 5,
                darkPactAttackPercentage: 0.2,
                darkPactHealthPercentage: 0.35
            );

            public static DragonStats Dragon = new DragonStats(
                attack: 50,
                strength: 1.5,
                armor: 35,
                health: 250,
                experienceOnDefeat: 1200,
                goldOnDefeat: 350,
                attackNames: new string[] { "Fire Breath", "Rock Throw", "Tail Strike" },
                fireBreathDamage: 10,
                burnChance: 33,
                burningDamage: 12,
                throwRockDamage: 20,
                stunChance: 25,
                tailStrikeDamage: 30
            );
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
                public static string[] miniBossTypes { get; } = new string[] { "Giant Spider", "Demonic Sorcerer" };
                public static int miniBossSpawnChance { get; } = 25;
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
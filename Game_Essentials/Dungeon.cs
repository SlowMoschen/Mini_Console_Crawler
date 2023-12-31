using Boss_Dragon;
using Game_Essentials;
using Game_Characters;
using _Items;
using Weapons;
using UserInput;

/**
*
*   Dungeon Generator
*   - Genreates a dungeon with a rooms and enemies according to the difficulty
*   - The Dungeon has a chest with gold and a possibility of a weapon and items
*
*   @param Room[] rooms - The rooms of the dungeon
*   @param int totalRooms - The total number of rooms in the dungeon
*   @param bool isBossDungeon - If the dungeon is a boss dungeon
*   @param Chest chest - The chest of the dungeon   
*
*/


namespace Dungeon_Generator {

    public class Dungeon {

        InputHandler InputHandler = new InputHandler();
        public Room[] rooms { get; set; }
        public int totlaRooms { get; set; }
        public bool isBossDungeon { get; set; }
        public Chest chest { get; set; }

        public Dungeon(string difficulty) {
            this.rooms = generateRooms(difficulty);
            this.totlaRooms = this.rooms.Length;
            this.chest = new Chest(difficulty);
            this.isBossDungeon = difficulty == "Boss" ? true : false;
        }
            
            public static Room[] generateRooms(string difficulty) {

                int rooms;
                switch (difficulty)
                {
                    case "Easy":
                        rooms = GameVariables.GameSettings.DungeonSettings.easyRooms;
                        break;
                    case "Medium":
                        rooms = GameVariables.GameSettings.DungeonSettings.mediumRooms;
                        break;
                    case "Hard":
                        rooms = GameVariables.GameSettings.DungeonSettings.hardRooms;
                        break;
                    case "Boss":
                        rooms = GameVariables.GameSettings.DungeonSettings.bossRooms;
                        break;
                    case "Dev":
                        rooms = 1;
                        break;
                    default:
                        rooms = 1;
                        break;
                }
                Room[] rooms = new Room[rooms];
    
                for (int i = 0; i < rooms; i++)
                {
                    rooms[i] = new Room(i + 1, difficulty);
                }
    
                return rooms;
            }


        // Method used to open the chest at the end of the dungeon
        // Could be refactored so that the Console.WriteLine() are in the DisplayManager
        public void openChest(Player player) {
            // If chest has no items or weapons, only gold
            if(this.chest.items.Length == 0 && this.chest.weapon == null) 
            {
                Console.WriteLine(" You found a chest with Items!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
            } 

            // If Chest has a weapon and items
            if(this.chest.weapon != null && this.chest.items.Length > 0) 
            {
                Console.WriteLine(" You found a chest with a weapon and Items!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
                Console.WriteLine(" You found " + this.chest.items.Length + " items!");
                foreach (Item item in this.chest.items)
                {
                    Console.WriteLine(" You found " + item.type + "!");
                    player.InventoryManager.addItem(item);
                }
                Console.WriteLine(" You found " + this.chest.weapon.name + "!");
                this.chest.weapon.printWeaponStats();
                string changeWeapon = InputHandler.getChoice(" Would you like to change your weapon?", new string[] {"Yes", "No"});
                if(changeWeapon == "Yes") {
                    player.currentWeapon = this.chest.weapon;
                    player.setAttackOptions();
                } else {
                    Console.WriteLine(" You left the weapon in the chest.");
                }
            } 

            // If Chest has a weapon and no items
            if (this.chest.weapon != null && this.chest.items.Length == 0) 
            {
                Console.WriteLine(" You found a chest with a weapon!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
                Console.WriteLine(" You found " + this.chest.weapon.name + "!");
                this.chest.weapon.printWeaponStats();
                string changeWeapon = InputHandler.getChoice(" Would you like to change your weapon?", new string[] {"Yes", "No"});
                if(changeWeapon == "Yes") {
                    player.currentWeapon = this.chest.weapon;
                    player.setAttackOptions();
                } else {
                    Console.WriteLine(" You left the weapon in the chest.");
                }
            }

            // If Chest has no weapon but items
            if(this.chest.weapon == null && this.chest.items.Length > 0) 
            {
                Console.WriteLine(" You found a chest with Items!");
                Console.WriteLine(" You found " + this.chest.gold + " gold!");
                player.InventoryManager.gold += this.chest.gold;
                Console.WriteLine(" You found " + this.chest.items.Length + " items!");
                foreach (Item item in this.chest.items)
                {
                    Console.WriteLine(" You found " + item.type + "!");
                    player.InventoryManager.addItem(item);
                }
            }
        }
    }

    public class Room {

        public int roomID { get; set; }
        public Enemy[] roomEnemies { get; set; }

        public Room(int roomID, string difficulty) {
            this.roomID = roomID;
            this.roomEnemies = generateRoom(difficulty);
        }

        // Generate a room of enemies
        public static Enemy[] generateRoom(string difficulty) {
            Enemy[] enemies;
            switch (difficulty)
            {
                case "Easy":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.easyMobs);
                    break;
                case "Medium":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.mediumMobs);
                    break;
                case "Hard":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.hardMobs);
                    break;
                case "Boss":
                    enemies = generateMobs(GameVariables.GameSettings.DungeonSettings.bossMobs, isBossDungeon: true);
                    break;
                case "Dev":
                    enemies = generateMobs(2, true);
                    break;
                default:
                    enemies = generateMobs(1);
                    break;
            }
            return enemies;
        }

        // Generate a number of random enemies
        // If the dungeon is a boss dungeon, the last enemy will be a boss
        public static Enemy[] generateMobs(int count, bool isBossDungeon = false)
        {
            Enemy[] enemies = new Enemy[count];
            string[] enemytypes = GameVariables.GameSettings.DungeonSettings.enemyTypes;
            Random random = new Random();
            int index = random.Next(enemytypes.Length);

            string enemyType = enemytypes[index];

            for (int i = 0; i < count; i++)
            {

                if(isBossDungeon && i == count - 1) {
                    enemies[i] = new Dragon(
                        "Dragon Boss",
                        attack: GameVariables.EnemyStats.Dragon.attack,
                        strength: GameVariables.EnemyStats.Dragon.strength,
                        armor: GameVariables.EnemyStats.Dragon.armor,
                        health: GameVariables.EnemyStats.Dragon.health,
                        experienceOnDefeat: GameVariables.EnemyStats.Dragon.experienceOnDefeat,
                        goldOnDefeat: GameVariables.EnemyStats.Dragon.goldOnDefeat
                    );
                    // enemies[i] = new _StoneGolem.StoneGolem(
                    //     "Stone Golem Boss",
                    //     attack: GameVariables.EnemyStats.StoneGolem.attack,
                    //     strength: GameVariables.EnemyStats.StoneGolem.strength,
                    //     armor: GameVariables.EnemyStats.StoneGolem.armor,
                    //     health: GameVariables.EnemyStats.StoneGolem.health,
                    //     experienceOnDefeat: GameVariables.EnemyStats.StoneGolem.experienceOnDefeat,
                    //     goldOnDefeat: GameVariables.EnemyStats.StoneGolem.goldOnDefeat
                    // );
                    continue;
                }

                switch (enemyType)
                {
                    case "Spider":
                        enemies[i] = new _Spider.Spider(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Zombie":
                        enemies[i] = new Zombie.Zombie(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Spider.attack,
                            strength: GameVariables.EnemyStats.Spider.strength,
                            armor: GameVariables.EnemyStats.Spider.armor,
                            health: GameVariables.EnemyStats.Spider.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Spider.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Spider.goldOnDefeat
                        );
                        break;
                    case "Goblin":
                        enemies[i] = new _Goblin.Goblin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Goblin.attack,
                            strength: GameVariables.EnemyStats.Goblin.strength,
                            armor: GameVariables.EnemyStats.Goblin.armor,
                            health: GameVariables.EnemyStats.Goblin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Goblin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Goblin.goldOnDefeat
                        );
                        break;
                    case "Assassin":
                        enemies[i] = new _Assassin.Assassin(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.Assassin.attack,
                            strength: GameVariables.EnemyStats.Assassin.strength,
                            armor: GameVariables.EnemyStats.Assassin.armor,
                            health: GameVariables.EnemyStats.Assassin.health,
                            experienceOnDefeat: GameVariables.EnemyStats.Assassin.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.Assassin.goldOnDefeat
                        );
                        break;
                    case "Stone Golem":
                        enemies[i] = new _StoneGolem.StoneGolem(
                            enemytypes[index],
                            attack: GameVariables.EnemyStats.StoneGolem.attack,
                            strength: GameVariables.EnemyStats.StoneGolem.strength,
                            armor: GameVariables.EnemyStats.StoneGolem.armor,
                            health: GameVariables.EnemyStats.StoneGolem.health,
                            experienceOnDefeat: GameVariables.EnemyStats.StoneGolem.experienceOnDefeat,
                            goldOnDefeat: GameVariables.EnemyStats.StoneGolem.goldOnDefeat
                        );
                        break;
                }
            }

            return enemies;
        }
    }

    public class Chest {
        public Item[] items { get; set; }
        public Weapon weapon { get; set; }
        public int gold { get; set; }

        public Chest(string difficulty) {
            this.items = generateItems(difficulty);
            this.weapon = generateWeapon();
            this.gold = generateGold(difficulty);
        }

        public static Weapon generateWeapon() {
            string[] weaponTypes = GameVariables.GameSettings.DungeonSettings.chestWeapons;

            //25% Chance to generate a weapon
            if(GameVariables.getChance(25)) {
                Random random = new Random();
                int index = random.Next(weaponTypes.Length);

                string weaponType = weaponTypes[index];

                switch (weaponType)
                {
                    case "Sword":
                        return new Sword(
                                name: "Sword",
                                attack: GameVariables.WeaponStats.Sword.getAttack(),
                                enduranceCost: GameVariables.WeaponStats.Sword.enduranceCost,
                                specialAttackEnduranceCost: GameVariables.WeaponStats.Sword.specialAttackEnduranceCost
                            );
                    case "Axe":
                        return new Axe(
                                name: "Axe",
                                attack: GameVariables.WeaponStats.Axe.getAttack(),
                                enduranceCost: GameVariables.WeaponStats.Axe.enduranceCost,
                                specialAttackEnduranceCost: GameVariables.WeaponStats.Axe.specialAttackEnduranceCost
                            );
                    case "Mace":
                        return new Mace(
                                name: "Mace",
                                attack: GameVariables.WeaponStats.Mace.getAttack(),
                                enduranceCost: GameVariables.WeaponStats.Mace.enduranceCost,
                                specialAttackEnduranceCost: GameVariables.WeaponStats.Mace.specialAttackEnduranceCost
                            );
                    default:
                    return new Sword(
                            name: "Sword",
                            attack: GameVariables.WeaponStats.Sword.getAttack(),
                            enduranceCost: GameVariables.WeaponStats.Sword.enduranceCost,
                            specialAttackEnduranceCost: GameVariables.WeaponStats.Sword.specialAttackEnduranceCost
                        );
                }
            }

            // If chance fails, return null
            return null;
        }

        public static int generateGold(string difficulty) {
            int gold = 0;
            switch (difficulty)
            {
                case "Easy":
                    gold = GameVariables.GameSettings.DungeonSettings.easyGold;
                    break;
                case "Medium":
                    gold = GameVariables.GameSettings.DungeonSettings.mediumGold;
                    break;
                case "Hard":
                    gold = GameVariables.GameSettings.DungeonSettings.hardGold;
                    break;
                case "Boss":
                    gold = GameVariables.GameSettings.DungeonSettings.bossGold;
                    break;
                case "Dev":
                    gold = 1000;
                    break;
                default:
                    gold = 0;
                    break;
            }
            return gold;
        }

        // genrates the Items based on
        public static Item[] generateItems(string difficulty) {
            int length = GameVariables.GameSettings.DungeonSettings.getChestItemsCount(difficulty);
            if(difficulty == "Dev") {
                length = 5;
            }
            Item[] items = new Item[length];

            if(difficulty == "Boss" || difficulty == "Hard") {
                for (int i = 0; i < length; i++)
                {
                    items[i] = generateItem(difficulty);
                }

                return items;
            }

            //25% Chance to generate Items for Easy and Medium Dungeons
            if(GameVariables.getChance(25)) {
                for (int i = 0; i < length; i++)
                {
                    items[i] = generateItem(difficulty);
                }

                return items;
            }

            // If chance fails, return empty array
            return [];
        }

        public static Item generateItem(string difficulty) {
            string[] itemTypes = GameVariables.GameSettings.DungeonSettings.chestItems;
            Random random = new Random();
            int index = random.Next(itemTypes.Length);

            string itemType = itemTypes[index];

            switch (itemType)
            {
                case "Health Potion":
                    return new Potion(
                            name: "Endurance Potion",
                            type: "Endurance Potion",
                            description: "Increases the Player's endurance by " + GameVariables.GameSettings.endurancePotionEnduranceRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.endurancePotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.endurancePotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.endurancePotionEnduranceRating
                        );
                case "Endurance Potion":
                    return new Potion(
                            name: "Heal Potion", 
                            type: "Health Potion", 
                            description: "Heals the Player for " + GameVariables.GameSettings.healPotionHealRating + " health", 
                            price: GameVariables.GameSettings.ItemPrices.healPotionPrice, 
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.healPotionMaxQuantity, 
                            effectValue: GameVariables.GameSettings.healPotionHealRating
                        );
                case "Strength Potion":
                    return new Potion(
                            name: "Strength Potion",
                            type: "Strength Potion",
                            description: "Increases the Player's strength by " + GameVariables.GameSettings.strengthPotionStrengthRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.strengthPotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.strengthPotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.strengthPotionStrengthRating
                        );
                default:
                    return new Potion(
                            name: "Endurance Potion",
                            type: "Endurance Potion",
                            description: "Increases the Player's endurance by " + GameVariables.GameSettings.endurancePotionEnduranceRating + " for " + GameVariables.GameSettings.EffectDurations.strengthDuration + " turns",
                            price: GameVariables.GameSettings.ItemPrices.endurancePotionPrice,
                            maxQuantity: GameVariables.GameSettings.ItemMaxQuantity.endurancePotionMaxQuantity,
                            effectValue: GameVariables.GameSettings.endurancePotionEnduranceRating
                        );
            }
        }
    }
}
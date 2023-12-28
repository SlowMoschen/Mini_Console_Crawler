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
            public static string version { get; } = "0.1.1";
            public static int surviedRooms { get; set;} = 0;
            public static int survivedDungeons { get; set;} = 0;
            public static int killedEnemies { get; set;} = 0;
            public static int totalExperience { get; set;} = 0;
            public static int totalGold { get; set;} = 0;
        }

        public class GameSettings {
            
            public static string playerName { get; set;} = "Player";
            public static int healPotionHealRating { get; } = 20;
            public static double strengthPotionStrengthRating { get; } = 2.0;
            public static int endurancePotionEnduranceRating { get; } = 50;
            public static int enduranceRegeneration { get; } = 7;
            public static int poisonDamage { get; } = 5;
            
            public class EffectDurations {
                public static int poisonDuration { get; } = 3;
                public static int strengthDuration { get; } = 7;
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
                public static string[] enemyTypes { get; } = new string[] { "Zombie", "Spider" };
                public static int easyRooms { get; } = 1;
                public static int mediumRooms { get; } = 2;
                public static int hardRooms { get; } = 3;
                public static int easyMobs { get; } = 1;
                public static int mediumMobs { get; } = 3;
                public static int hardMobs { get; } = 5;
            }
        }

        public class GameLoopBooleans {
            public static bool isInMenu { get; set;} = true;
            public static bool isInShop { get; set;} = false;
            public static bool isInFight { get; set;} = false;
            public static bool isDead { get; set;} = false;
            public static bool isDungeonCleared { get; set;} = false;
        }

        // Class will be initialized in a Player instance
        public class PlayerInventory {
            public int maxHealPotions { get; } = 5;
            public int maxEndurancePotions { get; } = 5;
            public int maxStrengthPotions { get; } = 5;

            public int healPotions { get; set;} = 0;
            public int endurancePotions { get; set;} = 0;
            public int strengthPotions { get; set;} = 1;
            public int gold { get; set;} = 0;
        }

    }

    public class DisplayManager {

        InputHandler InputHandler = new InputHandler();


        /*
        /
        /    Methods for Displaying Logic before the game starts
        /
        */

        public void displayGameLogo() {
            Console.Clear();
            Console.WriteLine(" ---------------------------");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |       Console_RPG       |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |   A simple console RPG  |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" ---------------------------");
            Console.WriteLine(" Version: " + GameVariables.GameStats.version);
            Console.WriteLine();
        }

        public void displayGreetings() {
            Console.Clear();
            this.displayGameLogo();
            Console.WriteLine(" Welcome to Console_RPG!");
            Console.WriteLine(" The goal of this game is to defeat all the enemies in the hardest Dungeon.");
            this.waitForInput();
            Console.Clear();
        }

        // Method to get the player name before the game starts
        public void getPlayerName() {
            Console.Clear();
            this.displayGameLogo();
            Console.WriteLine(" What is the Name of your Hero?");
            string playerName = Console.ReadLine();
            GameVariables.GameSettings.playerName = playerName;
            Console.WriteLine();
            Console.WriteLine(" Your Hero is called " + playerName);
            this.waitForInput();
            Console.Clear();
        }

        // Method to get the player weapon before the game starts
        public string getPlayerWeapon() {
            Console.Clear();
            this.displayGameLogo();
            string playerWeapon = InputHandler.getChoice(" What weapon will your Hero use?", new string[] { "Sword", "Axe", "Mace" });
            Console.WriteLine();
            Console.WriteLine(" Your Hero will use a " + playerWeapon);
            this.waitForInput();
            Console.Clear();
            return playerWeapon;
        }

        /*
        /
        /    Methods for Displaying Menu Logic
        /
        */

        public string displayMainMenu() {
            Console.Clear();
            this.displayHeader("Main Menu");
            string menuChoice = this.displayOptionMenu(" What would you like to do?", GameVariables.GameSettings.Options.mainMenuOptions);
            return menuChoice;
        }

        public void displayShop(Player player) {
            Console.Clear();
            this.displayHeader("Shop");
            Console.WriteLine(" You have " + player.inventory.gold + " gold");
            string menuChoice = this.displayOptionMenu(" What would you like to do?", GameVariables.GameSettings.Options.shopMenuOptions);
            
            switch (menuChoice) {
                case "Buy":
                    string itemChoice = this.displayOptionMenu(" What would you like to buy?", GameVariables.GameSettings.Options.shopItems);
                    switch (itemChoice) {
                        case "Heal Potion":
                            if(player.inventory.healPotions < player.inventory.maxHealPotions) {
                                if(player.inventory.gold >= 10) {
                                    player.inventory.healPotions++;
                                    player.inventory.gold -= 10;
                                    Console.WriteLine(" You bought a Heal Potion");
                                } else {
                                    Console.WriteLine(" You don't have enough gold to buy a Heal Potion");
                                }
                            } else {
                                Console.WriteLine(" You can't carry any more Heal Potions");
                            }
                            break;
                        case "Strength Potion":
                            if(player.inventory.strengthPotions < player.inventory.maxStrengthPotions) {
                                if(player.inventory.gold >= 20) {
                                    player.inventory.strengthPotions++;
                                    player.inventory.gold -= 20;
                                    Console.WriteLine(" You bought a Strength Potion");
                                } else {
                                    Console.WriteLine(" You don't have enough gold to buy a Strength Potion");
                                }
                            } else {
                                Console.WriteLine(" You can't carry any more Strength Potions");
                            }
                            break;
                        case "Endurance Potion":
                            if(player.inventory.endurancePotions < player.inventory.maxEndurancePotions) {
                                if(player.inventory.gold >= 30) {
                                    player.inventory.endurancePotions++;
                                    player.inventory.gold -= 30;
                                    Console.WriteLine(" You bought a Endurance Potion");
                                } else {
                                    Console.WriteLine(" You don't have enough gold to buy a Endurance Potion");
                                }
                            } else {
                                Console.WriteLine(" You can't carry any more Endurance Potions");
                            }
                            break;
                    }
                    break;
                case "Exit":
                    GameVariables.GameLoopBooleans.isInShop = false;
                    GameVariables.GameLoopBooleans.isInMenu = true;
                    Console.WriteLine(" You left the Shop");
                    break;
            }

        }



        /*
        /
        /    Helper Methods
        /
        */

        // Method to wait for user input before continuing
        public void waitForInput(string message = " Press any key to continue...") {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        // Method to display a header with a message
        // Calulate the padding based on the message length
        // Example: displayHeader("Hello World") will display:
        // ---------------------------
        //        Hello World
        // ---------------------------
        public void displayHeader(string message) {
            // Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------");

            int dashedLineLength = 27;
            int padding = (dashedLineLength - message.Length) / 2;

            Console.WriteLine("{0," + ((dashedLineLength - message.Length) / 2 + message.Length) + "}", message);

            Console.WriteLine("---------------------------");
            Console.WriteLine();
        }

        // Method to display a menu with a message and options
        public string displayOptionMenu(string message ,string[] options) {
            Console.WriteLine();
            string playerChoice = InputHandler.getChoice(message, options);
            Console.WriteLine();
            return playerChoice;
        }

        /*
        /
        /    Methods for Displaying Logic during the Battle
        /
        */

        public  void displayNewEncounter(string enemyName) {
            this.displayHeader("New Encounter");
            Console.WriteLine(" You encountered a " + enemyName);
        }

        public void displayGameStats() {
            this.displayHeader("Game Stats");
            Console.WriteLine(" Survived Rooms: " + GameVariables.GameStats.surviedRooms);
            Console.WriteLine(" Killed Enemies: " + GameVariables.GameStats.killedEnemies);
            Console.WriteLine(" Total Experience gained: " + GameVariables.GameStats.totalExperience);
            Console.WriteLine();
        }

        public void displayBattleStats(Player player, Enemy enemy) {
            Console.WriteLine();
            player.printBattleStats();
            this.displayHeader("VS");
            enemy.printBattleStats();
        }

        public void displayEnteredRoom( int roomID, int roomCount,int enemiesCount, int totalEnemiesCount) {
            this.displayHeader("New Room");
            Console.WriteLine(" You entered Room " + roomID + " of " + roomCount + " in this Dungeon");
            string ZombieOrZombies = enemiesCount > 1 ? "Zombies" : "Zombie";
            Console.WriteLine(" You have to defeat " + totalEnemiesCount + " " + ZombieOrZombies + " to get to the end of the Dungeon");
        }

        public void displayEnteredDungeon(string difficulty) {
            this.displayHeader("New Dungeon");
            Console.WriteLine(" You entered the " + difficulty + " Dungeon");
            Console.WriteLine(" You have to defeat all the enemies to get to the end of the Dungeon");
        }

        public void displayBattleOutcome(string playerChoice, string enemyChoice, Player player, Enemy enemy, string attackChoice)
        {
            double playerDamage = player.currentWeapon.attack * player.strength / enemy.armor;
            double playerKickDamage = (player.attack + player.kickAttackStrength) * player.strength / enemy.armor;
            double playerSpecialAttackDamage = (player.currentWeapon.specialAttackStrength + player.currentWeapon.attack) * player.strength / enemy.armor;
            double enemyDamage = enemy.attack * enemy.strength / player.armor;
            double enemyHeal = enemyDamage / 2;
            double enemySelfDamage = enemyDamage / 4;

                switch (playerChoice)
                {
                    case "Attack":     
                        if(!enemy.isDefending) {
                            if(player.endurance >= player.currentWeapon.enduranceCost) {
                                if(attackChoice == "Normal Attack") {
                                    Console.WriteLine($" You attacked the enemy for {playerDamage} damage");
                                } else if(attackChoice == "Kick Attack") {
                                    Console.WriteLine($" You kicked the enemy for {playerKickDamage} damage");
                                } else if(attackChoice == player.currentWeapon.specialAttackName) {
                                    Console.WriteLine($" You used {player.currentWeapon.specialAttackName} for {playerSpecialAttackDamage} damage");
                                }
                            } else {
                                Console.WriteLine(" You don't have enough endurance to attack");
                                break;
                            }
                        } else {
                            enemy.isDefending = false;
                        }
                        break;
                    case "Rest":
                        Console.WriteLine($" You healed for {player.healRating} health and got {(GameVariables.GameSettings.enduranceRegeneration * 2) + GameVariables.GameSettings.enduranceRegeneration } endurance back.");
                        break;
                    case "Use Potion":
                        if(attackChoice == "Heal Potion") {
                            Console.WriteLine($" You healed for {GameVariables.GameSettings.healPotionHealRating} health");
                        } else if(attackChoice == "Strength Potion") {
                            Console.WriteLine(player.strength);
                            Console.WriteLine($" You gained strength for {GameVariables.GameSettings.EffectDurations.strengthDuration} turns");
                        } else if(attackChoice == "Endurance Potion") {
                            Console.WriteLine($" You gained {GameVariables.GameSettings.endurancePotionEnduranceRating} endurance");
                        }
                        break;
                    case "Defend":
                        Console.WriteLine(" You successfully defended the attack!");
                        break;
                    case "Run":
                        Console.WriteLine(" You ran away");
                        break;
                }
            
            

            if (enemy is Zombie.Zombie && !player.isDefending)
            {
                switch (enemyChoice)
                {
                    case "bite":
                        Console.WriteLine($" The zombie bit you for {enemyDamage} damage and healed for {enemyHeal} health");
                        break;
                    case "thrash":
                        Console.WriteLine($" The zombie thrashed you for {enemyDamage} damage, and took {enemySelfDamage} damage with it.");
                        break;
                    case "attack":
                        Console.WriteLine($" The zombie attacked you for {enemyDamage} damage");
                        break;
                    case "defend":
                        Console.WriteLine(" The zombie defended the attack");
                        break;
                }
            }

            if(enemy is _Spider.Spider && !player.isDefending) {
                switch (enemyChoice)
                {
                    case "spit":
                        Console.WriteLine($" The spider spit at you for {enemyDamage} damage");
                        if(player.isPoisoned) {
                            Console.WriteLine(" The spider poisoned you, you will take damage over time");
                        }
                        break;
                    case "attack":
                        Console.WriteLine($" The spider attacked you for {enemyDamage} damage");
                        break;
                    case "defend":
                        Console.WriteLine(" The spider defended the attack");
                        break;
                }
            }
        }

        public void displayBattleWith(Player player, Enemy enemy, string[] options, string[] attackOptions) {
                            Console.Clear();
                            this.displayNewEncounter(enemy.name);

                            while (player.health > 0 && enemy.health > 0)
                        {
                            Console.WriteLine();

                            this.displayBattleStats(player, enemy);

                            string enemyChoice = enemy.executeMove(player);
                            string playerChoice = this.displayOptionMenu(" What will you do?", options);
                            string attackChoice = "";
                            player.decrementBuffs();
                            
                            switch (playerChoice)
                            {
                                case "Attack":
                                    attackChoice = player.chooseAttack(enemy);
                                    break;
                                case "Rest":
                                    player.Rest();
                                    break;
                                case "Use Potion":
                                    attackChoice = this.displayOptionMenu(" What item do you want to use?", GameVariables.GameSettings.Options.shopItems);
                                    player.usePotion(attackChoice);
                                    break;
                                case "Defend":
                                    player.Defend(enemy);
                                    break;
                                case "Run":
                                    string isRunning = this.displayOptionMenu(" Are you sure you want to run?", new string[] { "Yes", "No" });
                                    if (isRunning == "Yes")
                                    {
                                        Console.WriteLine(" You ran away");
                                        GameVariables.GameLoopBooleans.isInMenu = true;
                                        GameVariables.GameLoopBooleans.isInFight = false;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                            }

                            if (!GameVariables.GameLoopBooleans.isInFight)
                            {
                                break;
                            }

                            if (enemy.health <= 0)
                            {
                                Console.WriteLine($" You defeated the {enemy.name}");
                                GameVariables.GameStats.killedEnemies++;
                                Console.WriteLine(" You gained " + enemy.experienceOnDefeat + " experience");
                                Console.WriteLine(" The Enemy dropped " + enemy.goldOnDefeat + " gold");
                                player.gainGold(enemy.goldOnDefeat);
                                player.gainExperience(enemy.experienceOnDefeat);
                                this.waitForInput();
                                break;
                            }
                            else
                            {
                                this.displayBattleOutcome(playerChoice, enemyChoice, player, enemy, attackChoice);

                                // Regenerate Endurance
                                if(player.endurance != player.maxEndurance) {
                                    player.endurance += GameVariables.GameSettings.enduranceRegeneration;
                                    if(player.endurance > player.maxEndurance) {
                                        player.endurance = player.maxEndurance;
                                    }
                                }

                                if (player.health <= 0)
                                {
                                    this.displayDefeat();

                                    GameVariables.GameLoopBooleans.isDead = true;
                                    GameVariables.GameLoopBooleans.isInMenu = true;
                                    GameVariables.GameLoopBooleans.isInFight = false;

                                    break;
                                }
                            }

                            this.waitForInput();
                            Console.Clear();
                        }
        }

        public void enterDungeon(Player player) {
            string fightChoice = this.displayOptionMenu(" In wich Dungeon do you want to go?", GameVariables.GameSettings.Options.difficultyOptions);

                    Room[] dungeon = DungeonGenerator.generateDungeon(fightChoice);
                    
                    this.displayEnteredDungeon(fightChoice);
                    int totalMobsInDungeon = dungeon.Sum(room => room.roomEnemies.Length);
                    this.waitForInput();
                    
                    foreach (Room room in dungeon)
                    {
                        this.displayEnteredRoom(room.roomID, dungeon.Length, room.roomEnemies.Length, totalMobsInDungeon);
                        this.waitForInput();
                        foreach (Enemy enemy in room.roomEnemies)
                        {
                            if(GameVariables.GameLoopBooleans.isInFight) {
                                this.displayBattleWith(player, enemy, GameVariables.GameSettings.Options.battleOptions, GameVariables.GameSettings.Options.attackOptions);
                            }
                        }
                        bool allMobsDefeated = room.roomEnemies.All(enemy => enemy.health <= 0);
                        if(allMobsDefeated) {
                            this.displayRoomVictory();
                            GameVariables.GameStats.surviedRooms++;
                        }
                        
                        bool allRoomsDefeated = dungeon.All(room => room.roomEnemies.All(zombie => zombie.health <= 0));
                        if(allRoomsDefeated) {
                            GameVariables.GameLoopBooleans.isDungeonCleared = true;
                            break;
                        }

                    }
                    

                    if(GameVariables.GameLoopBooleans.isDungeonCleared) {
                        this.displayDungeonVictory();
                        GameVariables.GameLoopBooleans.isInMenu = true;
                        GameVariables.GameLoopBooleans.isInFight = false;
                        GameVariables.GameLoopBooleans.isDungeonCleared = false;
                        GameVariables.GameStats.survivedDungeons++;
                    }
        }

        /*
        /
        /    Methods for Displaying Logic after the Battle
        /
        */

        public void displayDungeonVictory() {
            this.displayHeader("Dungeon Cleared");
            Console.WriteLine(" You defeated all the Enemies in this Dungeon");
            this.waitForInput(" Press any key to get back to the Main Menu");
        }

        public void displayRoomVictory() {
            this.displayHeader("Room Cleared");
            this.waitForInput(" Press any key to get back to enter the next Room");
        }

        public void displayDefeat() {
            this.displayHeader("You Died");
            this.displayGameStats();
            this.waitForInput(" Press any key to get back to the Main Menu");
        }
    }
}
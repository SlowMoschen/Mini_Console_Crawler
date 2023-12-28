using UserInput;
using Console_RPG;
using Game_Characters;

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
        }

        public class GameSettings {
            
            public static int healPotionHealRating { get; } = 20;
            public static string playerName { get; set;} = "Player";
            public static int maxEndurance { get; } = 100;
            public static int enduranceRegeneration { get; } = 7;
            public static int maxZombieCount { get; } = 3;
            public static int maxZombieAttack { get; } = 10;
            public static double maxZombieStrength { get; } = 1.0;
            public static int maxZombieArmor { get; } = 5;
            public static int maxZombieHealth { get; } = 50;
            public static int maxZombieExperience { get; } = 20;
            
            public class Options {
                public static string[] difficultyOptions { get; } = new string[] { "Easy", "Medium", "Hard" };
                public static string[] battleOptions { get; } = new string[] { "Attack", "Rest", "Defend", "Run" };
                public static string[]? attackOptions { get; set; }
                public static string[] mainMenuOptions { get; } = new string[] { "Enter Dungeon", "View Stats" };
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
            public static bool isInFight { get; set;} = false;
            public static bool isDead { get; set;} = false;
            public static bool isDungeonCleared { get; set;} = false;
        }

        public class PlayerInventory {
            public static int healPotions { get; set;} = 0;
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

        public string displayMainMenu() {
            Console.Clear();
            this.displayHeader("Main Menu");
            string menuChoice = this.displayOptionMenu(" What would you like to do?", GameVariables.GameSettings.Options.mainMenuOptions);
            return menuChoice;
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
                            
                            switch (playerChoice)
                            {
                                case "Attack":
                                    attackChoice = player.chooseAttack(enemy);
                                    break;
                                case "Rest":
                                    player.Rest();
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
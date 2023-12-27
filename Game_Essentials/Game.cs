using UserInput;
using Console_RPG;

namespace Game_Essentials {

    public class GameVariables {

        public class GameStats {
            public static string version { get; } = "0.1.1";
            public static int surviedRooms { get; set;} = 0;
            public static int survivedDungeons { get; set;} = 0;
            public static int killedEnemies { get; set;} = 0;
            public static int totalExperience { get; set;} = 0;
        }

        public class GameSettings {
            
            public static string playerName { get; set;} = "Player";
            public static int maxEndurance { get; } = 100;
            public static int enduranceRegeneration { get; } = 7;
            public static int maxZombieCount { get; } = 3;
            public static int maxZombieAttack { get; } = 10;
            public static double maxZombieStrength { get; } = 1.0;
            public static int maxZombieArmor { get; } = 5;
            public static int maxZombieHealth { get; } = 10;
            public static int maxZombieExperience { get; } = 20;
            
            public class Options {
                public static string[] difficultyOptions { get; } = new string[] { "Easy", "Medium", "Hard" };
                public static string[] battleOptions { get; } = new string[] { "Attack", "Heal", "Defend", "Run" };
                public static string[] attackOptions { get; set; }
                public static string[] mainMenuOptions { get; } = new string[] { "Enter Dungeon", "View Stats" };
            }

            public class DungeonSettings {
                public static int easyRooms { get; } = 1;
                public static int mediumRooms { get; } = 2;
                public static int hardRooms { get; } = 3;

                public class RoomSettings {
                    public static int easyEnemies { get; } = 1;
                    public static int mediumEnemies { get; } = 3;
                    public static int hardEnemies { get; } = 5;
                }
            }
        }

        public class GameLoopBooleans {
            public static bool isInMenu { get; set;} = true;
            public static bool isInFight { get; set;} = false;
            public static bool isDead { get; set;} = false;
            public static bool isDungeonCleared { get; set;} = false;
        }

    }

    public class DisplayManager {

        InputHandler InputHandler = new InputHandler();

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

        public void waitForInput(string message = " Press any key to continue...") {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }
        public void displayHeader(string message) {
            // Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------");

            int dashedLineLength = 27;
            // Calculate the padding needed to center the title
            int padding = (dashedLineLength - message.Length) / 2;

            // Print the title with the calculated padding
            Console.WriteLine("{0," + ((dashedLineLength - message.Length) / 2 + message.Length) + "}", message);

            Console.WriteLine("---------------------------");
            Console.WriteLine();
        }

        public  void displayNewEncounter(string enemyName) {
            this.displayHeader("New Encounter");
            Console.WriteLine(" You encountered a " + enemyName);
        }

        public void displayMenuText() {
            Console.Clear();
            this.displayHeader("Main Menu");
        }


        public string displayOptionMenu(string message ,string[] options) {
            Console.WriteLine();
            string playerChoice = InputHandler.getChoice(message, options);
            Console.WriteLine();
            return playerChoice;
        }

        public void displayGameStats() {
            this.displayHeader("Game Stats");
            Console.WriteLine(" Survived Rooms: " + GameVariables.GameStats.surviedRooms);
            Console.WriteLine(" Killed Enemies: " + GameVariables.GameStats.killedEnemies);
            Console.WriteLine(" Total Experience gained: " + GameVariables.GameStats.totalExperience);
            Console.WriteLine();
        }

        public void displayBattleStats(Player.Player player, Enemy.Enemy enemy) {
            Console.WriteLine();
            player.printBattleStats();
            this.displayHeader("VS");
            enemy.printBattleStats();
        }

        public void displayEnteredRoom( int roomNumber, int roomCount,int enemiesCount, int totalEnemiesCount) {
            this.displayHeader("New Room");
            Console.WriteLine(" You entered Room " + roomNumber + " of " + roomCount + " in this Dungeon");
            string ZombieOrZombies = enemiesCount > 1 ? "Zombies" : "Zombie";
            Console.WriteLine(" You have to defeat " + totalEnemiesCount + " " + ZombieOrZombies + " to get to the end of the Dungeon");
        }

        public void displayEnteredDungeon(string difficulty) {
            this.displayHeader("New Dungeon");
            Console.WriteLine(" You entered the " + difficulty + " Dungeon");
            Console.WriteLine(" You have to defeat all the enemies to get to the end of the Dungeon");
        }

        public void displayBattleOutcome(string playerChoice, string enemyChoice, Player.Player player, Person.Person enemy, string attackChoice)
        {
            double playerDamage = player.currentWeapon.attack * player.strength / enemy.armor;
            double playerKickDamage = (player.attack + player.kickAttackStrength) * player.strength / enemy.armor;
            double playerSpecialAttackDamage = (player.currentWeapon.specialAttackStrength + player.currentWeapon.attack) * player.strength / enemy.armor;
            double enemyDamage = enemy.attack * enemy.strength / player.armor;
            double enemyHeal = enemyDamage / 2;
            double enemySelfDamage = enemyDamage / 4;

            if(!enemy.isDefending) {
                switch (playerChoice)
                {
                    case "Attack":
                        if(attackChoice == "Normal Attack") {
                            Console.WriteLine($" You attacked the enemy for {playerDamage} damage");
                        } else if(attackChoice == "Kick Attack") {
                            Console.WriteLine($" You kicked the enemy for {playerKickDamage} damage");
                        } else if(attackChoice == player.currentWeapon.specialAttackName) {
                            Console.WriteLine($" You used {player.currentWeapon.specialAttackName} for {playerSpecialAttackDamage} damage");
                        }
                        break;
                    case "Heal":
                        Console.WriteLine($" You healed for {player.healRating} health");
                        break;
                    case "Defend":
                        Console.WriteLine(" You successfully defended the attack!");
                        break;
                    case "Run":
                        Console.WriteLine(" You ran away");
                        break;
                }
            } else {
                Console.WriteLine(" The damage was blocked by the Enemy");
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
                        Console.WriteLine(" The zombie is defending the next attack");
                        break;
                }
            }
        }

        public void displayBattleWith(Player.Player player, Zombie.Zombie zombie, string[] options, string[] attackOptions) {
                            Console.Clear();
                            this.displayNewEncounter(zombie.name);

                            while (player.health > 0 && zombie.health > 0)
                        {
                            Console.WriteLine();

                            this.displayBattleStats(player, zombie);

                            string playerChoice = this.displayOptionMenu(" What will you do?", options);
                            string attackChoice = "";
                            switch (playerChoice)
                            {
                                case "Attack":
                                    attackChoice = player.chooseAttack(zombie);
                                    break;
                                case "Heal":
                                    player.Heal();
                                    break;
                                case "Defend":
                                    player.Defend(zombie);
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
                                    break;
                                        }

                            if (!GameVariables.GameLoopBooleans.isInFight)
                            {
                                break;
                            }

                            if (zombie.health <= 0)
                            {
                                Console.WriteLine(" You defeated the zombie");
                                GameVariables.GameStats.killedEnemies++;
                                Console.WriteLine(" You gained " + zombie.experienceOnDefeat + " experience");
                                player.gainExperience(zombie.experienceOnDefeat);
                                this.waitForInput();
                                break;
                            }
                            else
                            {
                                string zombieChoice = zombie.executeMove(player);
                                this.displayBattleOutcome(playerChoice, zombieChoice, player, zombie, attackChoice);

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
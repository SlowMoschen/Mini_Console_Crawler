using UserInput;
using Console_RPG;

namespace Game_Methods {

    public class Game {

        InputHandler InputHandler = new InputHandler();
        GameState GameState = new GameState();

        public void displayGreetings() {
            Console.Clear();
            Console.WriteLine(" ---------------------------");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |       Console_RPG       |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" |   A simple console RPG  |");
            Console.WriteLine(" |                         |");
            Console.WriteLine(" ---------------------------");
            Console.WriteLine();
            Console.WriteLine(" Welcome to Console_RPG!");
            Console.WriteLine(" Your goal is to defeat the hardest Dungeon.");
            this.waitForInput();
            Console.Clear();
        }

        public void waitForInput(string message = " Press any key to continue...") {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        public  void displayNewEncounter(string enemyName) {
            Console.WriteLine();
            Console.WriteLine(" ------!New encoutner!------");
            Console.WriteLine(" You encountered a new " + enemyName + "!");
            Console.WriteLine(" ---------------------------");
            Console.WriteLine();
        }

        public void displayMenuText() {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("         Main Menu         ");
            Console.WriteLine("---------------------------");
        }

        public string displayOptionMenu(string message ,string[] options) {
            Console.WriteLine();
            string playerChoice = InputHandler.getChoice(message, options);
            Console.WriteLine();
            return playerChoice;
        }

        public void displayGameStats() {
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine("        Game Stats         ");
            Console.WriteLine("---------------------------");
            Console.WriteLine(" Survived Rooms: " + GameState.surviedRooms);
            Console.WriteLine(" Killed Enemies: " + GameState.killedEnemies);
            Console.WriteLine(" Total Experience gained: " + GameState.totalExperience);
            Console.WriteLine("---------------------------");
            Console.WriteLine();
        }

        public void displayBattleStats(Person.Person player, Enemy.Enemy enemy) {
            Console.WriteLine();
            player.printBattleStats();
            Console.WriteLine(" --------------------");
            Console.WriteLine("         VS          ");
            Console.WriteLine(" --------------------");
            enemy.printBattleStats();
        }

        public void displayEnteredRoom( int roomNumber, int roomCount,int enemiesCount, int totalEnemiesCount) {
            Console.WriteLine();
            Console.WriteLine(" You entered Room " + roomNumber + " of " + enemiesCount + " in this Dungeon");
            string ZombieOrZombies = enemiesCount > 1 ? "Zombies" : "Zombie";
            Console.WriteLine(" You have to defeat " + enemiesCount + " " + ZombieOrZombies + " to get to the end of the Dungeon");
        }

        public void displayEnteredDungeon(string difficulty) {
            Console.WriteLine();
            Console.WriteLine(" You entered the " + difficulty + " Dungeon");
            Console.WriteLine(" You have to defeat all the enemies to get to the end of the Dungeon");
        }

        public void displayBattleOutcome(string playerChoice, string enemyChoice, Player.Player player, Person.Person enemy)
        {
            double playerDamage = player.attack * player.strength / enemy.armor;
            double enemyDamage = enemy.attack * enemy.strength / player.armor;

            if(!enemy.isDefending) {
                switch (playerChoice)
                {
                    case "Attack":
                        Console.WriteLine($" You attacked the zombie for {playerDamage} damage");
                        break;
                    case "Slash Attack":
                        Console.WriteLine($" You slashed the zombie for {playerDamage} damage");
                        break;
                    case "Kick Attack":
                        Console.WriteLine($" You kicked the zombie for {playerDamage} damage");
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
                        Console.WriteLine($" The zombie bit you for {enemyDamage} damage");
                        break;
                    case "thrash":
                        Console.WriteLine($" The zombie thrashed you for {enemyDamage} damage");
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
                            switch (playerChoice)
                            {
                                case "Attack":
                                    string attackForm = this.displayOptionMenu(" Which attack will you use?", attackOptions);
                                    switch (attackForm)
                                    {
                                        case "Normal Attack":
                                            player.Attack(zombie);
                                            break;
                                        case "Slash Attack":
                                            player.slashAttack(zombie);
                                            break;
                                        case "Kick Attack":
                                            player.kickAttack(zombie);
                                            break;
                                    }
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
                                        GameState.isInMenu = true;
                                        GameState.isInFight = false;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                    break;
                                        }

                            if (!GameState.isInFight)
                            {
                                break;
                            }

                            if (zombie.health <= 0)
                            {
                                Console.WriteLine(" You defeated the zombie");
                                GameState.killedEnemies++;
                                Console.WriteLine(" You gained " + zombie.experienceOnDefeat + " experience");
                                player.gainExperience(zombie.experienceOnDefeat);
                                this.waitForInput();
                                break;
                            }
                            else
                            {
                                string zombieChoice = zombie.executeMove(player);
                                this.displayBattleOutcome(playerChoice, zombieChoice, player, zombie);
                                if (player.health <= 0)
                                {
                                    this.displayDefeat();

                                    GameState.isDead = true;
                                    GameState.isInMenu = true;
                                    GameState.isInFight = false;

                                    break;
                                }
                            }

                            this.waitForInput();
                            Console.Clear();
                        }
        }

        public void displayDungeonVictory() {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("         Victory!          ");
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            Console.WriteLine(" You defeated all the Enemies in this Dungeon");
            this.waitForInput(" Press any key to get back to the Main Menu");
        }

        public void displayRoomVictory() {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("       Room Cleared!       ");
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            this.waitForInput(" Press any key to get back to enter the next Room");
        }

        public void displayDefeat() {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("          You died!        ");
            Console.WriteLine("---------------------------");
            this.displayGameStats();
            this.waitForInput(" Press any key to get back to the Main Menu");
        }
    }
}
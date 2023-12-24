using UserInput;
using Console_RPG;

namespace WriteLine {

    public class WriteLine {

        InputHandler InputHandler = new InputHandler();
        GameState GameState = new GameState();

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

        public void displayBattleStats(Person.Person player, Person.Person enemy) {
            Console.WriteLine();
            player.printBattleStats();
            Console.WriteLine(" --------------------");
            Console.WriteLine("         VS          ");
            Console.WriteLine(" --------------------");
            enemy.printBattleStats();
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
                                            Console.WriteLine(" You gained " + zombie.experienceOnDefeat + " experience");
                                            player.gainExperience(zombie.experienceOnDefeat);
                                            Console.WriteLine();
                                            Console.WriteLine(" Press any key to continue");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else
                                        {
                                            string zombieChoice = zombie.executeMove(player);
                                            this.displayBattleOutcome(playerChoice, zombieChoice, player, zombie);
                                            if (player.health <= 0)
                                            {
                                                Console.WriteLine(" You died");
                                            }
                                        }


                            Console.WriteLine();
                            Console.WriteLine(" Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                        }
        }

        public void displayVictory() {
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine("         Victory!          ");
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            Console.WriteLine(" You defeated all the Enemies");
            Console.WriteLine();
            Console.WriteLine(" Press any key to get back to the Main Menu");
            Console.ReadKey();
        }
    }
}